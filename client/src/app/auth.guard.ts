import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { JWTTokenService } from './service/jwttoken.service';

export const authGuard: CanActivateFn = (route, state) => {
  return inject(JWTTokenService).getUser() ? true : inject(Router).createUrlTree(['landing']);
};
