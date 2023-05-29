import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Clientes, Facturas, ClientesFacturas, HistorialCliente } from '../models/Modelos';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private http: HttpClient
  ) { }

    //Url's
    //Clientes
    urlCrearCliente = "https://localhost:7104/api/APIs/GuardarCliente";
    urlVerClientes = "https://localhost:7104/api/APIs/Lista_Clientes";
    urlVerClienteDetalles = "https://localhost:7104/api/APIs/ObtenerCliente/";
    urlEditarClientes = "https://localhost:7104/api/APIs/EditarCliente";
    //Facturas
    urlCrearFacturas = "https://localhost:7104/api/APIs/GuardarFactura";
    urlVerFacturas = "https://localhost:7104/api/APIs/Lista_Factura";
    urlEditarFacturas = "https://localhost:7104/api/APIs/EditarFactura";
    urlVerFacturaDetalles = "https://localhost:7104/api/APIs/ObtenerFactura/";
    //Cliente-Factura
    urlVerClienteFacturas = "https://localhost:7104/api/APIs/ObtenerClienteFactura/";
    //HistorialCliente
    urlCrearHistorial = "https://localhost:7104/api/APIs/GuardarHistorial";
    urlVerHistorial = "https://localhost:7104/api/APIs/Lista_Historial";
    urlVerHistorialDetalles = "https://localhost:7104/api/APIs/ObtenerHistorialCliente/";


    //Funciones
    //Clientes
    agregarCliente(clientes:Clientes):Observable<Clientes>{
      return this.http.post<Clientes>(this.urlCrearCliente, clientes);
    }

    getAllClientes(){
      return this.http.get(this.urlVerClientes);
    }

    getClientes(clientes:Clientes){
      return this.http.get(this.urlVerClienteDetalles+`${clientes}`); 
    }

    updateClientes(clientes:Clientes):Observable<Clientes>{
      return this.http.put<Clientes>(this.urlEditarClientes, clientes);
    }

    //Facturas
    agregarFactura(facturas:Facturas):Observable<Facturas>{
      return this.http.post<Facturas>(this.urlCrearFacturas, facturas);
    }

    getAllFacturas(){
      return this.http.get(this.urlVerFacturas);
    }

    getFacturas(facturas:Facturas){
      return this.http.get(this.urlVerFacturaDetalles+`${facturas}`);
    }

    updateFacturas(idFactura:number, h_facturas:Facturas):Observable<Facturas>{
      return this.http.put<Facturas>(this.urlEditarFacturas + `${idFactura}`, h_facturas);
    }

    updateFactura(facturas:Facturas):Observable<Facturas>{
      return this.http.put<Facturas>(this.urlEditarFacturas, facturas);
    }

    //Clientes-Facturas
    getClientesFacturas(clientesfacturas:ClientesFacturas){
      return this.http.get(this.urlVerClienteFacturas+`${clientesfacturas}`);
    }

    //Historial Cliente
    agregarHistorial(HistorialCliente:HistorialCliente):Observable<HistorialCliente>{
      return this.http.post<HistorialCliente>(this.urlCrearHistorial, HistorialCliente);
    }

    getAllHistorial(){
      return this.http.get(this.urlVerHistorial);
    }

    getHistorial(historialcliente:HistorialCliente){
      return this.http.get(this.urlVerHistorialDetalles+`${historialcliente}`);
    }

}
