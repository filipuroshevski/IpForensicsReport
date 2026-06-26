using Data.Entities.IpForensicsReports;
using Data.Entities.Users;
using Data.Implementation;
using Data.Interfaces;
using Domain.Models.Errors;
using Domain.Models.IpForensicsReports.GenerateReport;
using Domain.Models.IpForensicsReports.ReportList;
using Domain.Models.Register;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interface.Common;
using Service.Interface.Common.DataProtection;
using Service.Interface.IpForensicsReport;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Service.Implementation.IpForensicsReports
{
    public class IpForensicsReportService : IIpForensicsReportService
    {
        #region Fields
        private readonly IRepository<IpForensicsReport> _ipForensicsReportRepository;
        private readonly ICommonService _commonService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IDataProtectionService _protector;
        #endregion

        #region Ctor
        public IpForensicsReportService(IRepository<IpForensicsReport> ipForensicsReportRepository,
            ICommonService commonService,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IDataProtectionService protector)
        {
            _ipForensicsReportRepository = ipForensicsReportRepository;
            _commonService = commonService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _protector = protector;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method for generate report for given ip address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task GenerateReport(GenerateReportModel model, string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var ipApiBaseUrl = _configuration["Apis:IpApiBaseUrl"];

            var geoResponse =
                await client.GetFromJsonAsync<IpApiResponseModel>(
                    $"{ipApiBaseUrl}json/{model.IpAddress}" +
                    "?fields=status,message,continent,country,regionName,city,mobile,proxy,hosting");

            if (geoResponse == null || geoResponse.Status != "success")
            {
                throw new ApplicationException(ErrorModel.IpAddressInvalid);
            }

            var abuseBaseUrl = _configuration["Apis:AbuseIpDbBaseUrl"];

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{abuseBaseUrl}check?ipAddress={model.IpAddress}&maxAgeInDays=90");

            request.Headers.Add("Key", _configuration["Apis:AbuseIpDbApiKey"]);
            request.Headers.Add("Accept", "application/json");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var abuseResponse =
                await response.Content.ReadFromJsonAsync<AbuseIpDbResponseModel>();

            var result = new GenerateReportResultModel
            {
                IpAddress = model.IpAddress,

                AbuseConfidenceScore = abuseResponse?.Data?.AbuseConfidenceScore ?? 0,
                TotalReports = abuseResponse?.Data?.TotalReports ?? 0,
                LastReportedDate = abuseResponse?.Data?.LastReportedAt,

                Continent = geoResponse.Continent,
                Country = geoResponse.Country,
                Region = geoResponse.RegionName,
                City = geoResponse.City,

                Mobile = geoResponse.Mobile,
                Proxy = geoResponse.Proxy,
                Hosting = geoResponse.Hosting
            };

            using (var scope = new UnitOfWork())
            {
                var entity = new IpForensicsReport
                {
                    UserId = userId,
                    IpAddress = _protector.Protect(result.IpAddress),

                    AbuseConfidenceScore = _protector.Protect(result.AbuseConfidenceScore.ToString()),
                    TotalReports = _protector.Protect(result.TotalReports.ToString()),
                    LastReportedDate = _protector.Protect(result.LastReportedDate?.ToString("o") ?? string.Empty),

                    Continent = _protector.Protect(result.Continent ?? string.Empty),
                    Country = _protector.Protect(result.Country ?? string.Empty),
                    Region = _protector.Protect(result.Region ?? string.Empty),
                    City = _protector.Protect(result.City ?? string.Empty),

                    Mobile = _protector.Protect(result.Mobile.ToString()),
                    Proxy = _protector.Protect(result.Proxy.ToString()),
                    Hosting = _protector.Protect(result.Hosting.ToString()),
                    Tor = _protector.Protect(result.Tor.ToString()),

                    CreatedDate = _protector.Protect(DateTime.UtcNow.ToString("o"))
                };

                await _ipForensicsReportRepository.InsertAsync(entity);

                scope.Commit();
            }
        }

        /// <summary>
        /// Get all reports for the given user with pagination and filtering
        /// </summary>
        /// <param name="searchReportModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ReportViewModel> GetAllReports(SearchReportModel searchReportModel, string userId)
        {
            var viewModel = new ReportViewModel();

            int totalRows = (await _ipForensicsReportRepository.FindAsyncQueryable(x => x.UserId == userId)).Count();
            viewModel.SearchReportModel = searchReportModel;
            viewModel.SearchReportModel.TotalPages = _commonService.CalculateTotalPages(totalRows, viewModel.SearchReportModel.PageSize);
            viewModel.SearchReportModel.TotalRows = totalRows;

            var reports = (await _ipForensicsReportRepository.FindAsyncQueryable(x => x.UserId == userId))
                .OrderByDescending(x => x.Id)
                .Skip((searchReportModel.CurrentPageNumber - 1) * searchReportModel.PageSize)
                .Take(searchReportModel.PageSize)
                .ToList();

            viewModel.ReportBaseModels = reports
                .Select(x => new ReportBaseModel
                {
                    Id = x.Id,

                    IpAddress = _protector.Unprotect(x.IpAddress),

                    AbuseConfidenceScore = _protector.Unprotect(x.AbuseConfidenceScore),
                    TotalReports = _protector.Unprotect(x.TotalReports),
                    LastReportedDate = string.IsNullOrEmpty(x.LastReportedDate) ? string.Empty
                    : Convert.ToDateTime(_protector.Unprotect(x.LastReportedDate)).ToString("dd/MM/yyyy"),

                    Continent = _protector.Unprotect(x.Continent),
                    Country = _protector.Unprotect(x.Country),
                    Region = _protector.Unprotect(x.Region),
                    City = _protector.Unprotect(x.City),

                    Mobile = _protector.Unprotect(x.Mobile),
                    Proxy = _protector.Unprotect(x.Proxy),
                    Hosting = _protector.Unprotect(x.Hosting),
                    Tor = _protector.Unprotect(x.Tor),

                    CreatedDate = Convert.ToDateTime(_protector.Unprotect(x.CreatedDate)).ToString("dd/MM/yyyy")
                })
                .ToList();

            return viewModel;
        }
        #endregion
    }
}


