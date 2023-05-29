import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './components/clientes/clientes.component';
import { ClientesVencidosComponent } from './components/clientes-vencidos/clientes-vencidos.component';
import { HomeComponent } from './components/home/home.component';
import { PrestamosComponent } from './components/prestamos/prestamos.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'clientes', component: ClientesComponent },
  { path: 'prestamos', component: PrestamosComponent },
  { path: 'clientesVencidos', component: ClientesVencidosComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
