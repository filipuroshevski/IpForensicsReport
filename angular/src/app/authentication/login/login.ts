import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';
import { UtilsService } from '../../shared/common/services/utils.service';
import { LoginModel } from '../../shared/models/login/login.model';
import { SystemErrorModel } from '../../shared/models/system-error/system-error.model';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  
}
