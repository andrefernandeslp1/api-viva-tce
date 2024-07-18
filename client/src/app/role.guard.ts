import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { JWTTokenService } from './service/jwttoken.service';

export const roleGuard: CanActivateFn = (route, state) => {

  const jwtTokenService = inject(JWTTokenService);
  const router  = inject(Router);

  const role = jwtTokenService.getRole();
  const expectedRoles = route.data['expectedRoles'];

  if (!role || !expectedRoles.includes(role)) {
    return router.createUrlTree(['unauthorized']);
  }
  else {
    return true;
  }
};
