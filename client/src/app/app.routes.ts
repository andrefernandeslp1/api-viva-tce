import { Routes } from '@angular/router';
import { ListServicoComponent } from './components/servico/list-servico/list-servico.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { FormServicoComponent } from './components/servico/form-servico/form-servico.component';
import { authGuard } from './auth.guard';
import { roleGuard } from './role.guard';
import { DetalhesServicoComponent } from './components/servico/detalhes-servico/detalhes-servico.component';
import { ListUsuarioComponent } from './components/usuario/list-usuario/list-usuario.component';
import { FormUsuarioComponent } from './components/usuario/form-usuario/form-usuario.component';
import { PerfilUsuarioComponent } from './components/usuario/perfil-usuario/perfil-usuario.component';
import { FormFornecedorComponent } from './components/fornecedor/form-fornecedor/form-fornecedor.component';
import { ListFornecedorComponent } from './components/fornecedor/list-fornecedor/list-fornecedor.component';
import { PerfilFornecedorComponent } from './components/fornecedor/perfil-fornecedor/perfil-fornecedor.component';
import { LandingComponent } from './components/landing/landing.component';
import { HeaderComponent } from './components/header/header.component';

export const routes: Routes = [

  { path: '', redirectTo: '/landing', pathMatch: 'full' },

  { path: 'landing', component: LandingComponent },

  { path: 'unauthorized', component: UnauthorizedComponent },

  { path: 'home',
    component: ListServicoComponent,
    canActivate: [authGuard]
  },

  // SERVIÇO
  { path: 'servico',
    component: ListServicoComponent,
    canActivate: [authGuard]
  },
  {
    path: 'servico/form',
    component: FormServicoComponent,
    canActivate: [authGuard, roleGuard],
    data: {expectedRoles: ['fornecedor']}
  },
  {
    path: 'servico/:id/detalhes',
    component: DetalhesServicoComponent,
    canActivate: [authGuard]
  },

    // USUÁRIO
    {
      path: 'usuario',
      component: ListUsuarioComponent,
      canActivate: [authGuard, roleGuard],
      data: {expectedRoles: ['admin']}
    },
    {
      path: 'usuario/form',
      component: FormUsuarioComponent,
      canActivate: [authGuard, roleGuard],
      data: {expectedRoles: ['admin']}
    },
    {
      path: 'usuario/:id/perfil',
      component: PerfilUsuarioComponent,
      canActivate: [authGuard, roleGuard],
      data: {expectedRoles: ['admin', 'client']}
    },

    // FORNECEDOR
    {
      path: 'fornecedor',
      component: ListFornecedorComponent,
      canActivate: [authGuard, roleGuard],
      data: {expectedRoles: ['admin', 'client']}
    },
    {
      path: 'fornecedor/form',
      component: FormFornecedorComponent,
      canActivate: [authGuard, roleGuard],
      data: {expectedRoles: ['admin']}
    },
    {
      path: 'fornecedor/:id/perfil',
      component: PerfilFornecedorComponent,
      canActivate: [authGuard, roleGuard],
      data: {expectedRoles: ['admin', 'client']}
    },

    { path: 'header', component: HeaderComponent },

];
