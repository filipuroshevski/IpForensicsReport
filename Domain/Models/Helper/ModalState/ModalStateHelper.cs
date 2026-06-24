using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using FluentValidation.Results;

namespace Domain.Models.Helper.ModalState
{
    public static class ModalStateHelper
    {
        public static string ModalStateException(ValidationResult validationResult)
        {
            var validationErrors = new List<ValidationError>();

            foreach (var error in validationResult.Errors)
            {
                var validationError = new ValidationError()
                {
                    Key = error.PropertyName,
                    Message = error.ErrorMessage
                };
                validationErrors.Add(validationError);
            }

            var json = JsonSerializer.Serialize(validationErrors);

            return json;
        }


    }
}
