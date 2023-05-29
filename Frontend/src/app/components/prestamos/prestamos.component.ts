import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounce, debounceTime } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Facturas, Clientes, ClientesFacturas, HistorialCliente } from 'src/app/models/Modelos';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-prestamos',
  templateUrl: './prestamos.component.html',
  styleUrls: ['./prestamos.component.css']
})
export class PrestamosComponent {
  c_facturas: Facturas = new Facturas();
  c_clientes: Clientes = new Clientes();
  c_clienteFactura: ClientesFacturas = new ClientesFacturas();
  c_historial: HistorialCliente = new HistorialCliente();
  u_factura: Facturas = new Facturas();
  control = new FormControl();
  resultado = 0;
  cantidadone = 0;
  //cantidadtwo = this.c_facturas.MONTO_SOLICITADO;
  public clientes: Array<any> = []
  public facturas: Array<any> = []
  public historial: Array<any> = []
  constructor(
    private apiService: ApiService,
    public modal: NgbModal,
    public modal2: NgbModal,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
      this.ObserverChangeSearch();
      this.ObserverChangeSearch2();
      this.ObserverChangeSearch3();
  }

  //Clientes
  ObserverChangeSearch(){
    this.control.valueChanges
      .pipe(
        debounceTime(500)
      )
      .subscribe(query=> {
        console.log(query);
        this.apiService.getClientes(query).subscribe(
          (res:any) => {
            this.clientes = res;
          },
          (object) => {
            console.log(object)
          }
        )
      })
  }

  //Modals
    //Modal Prestamo
    onAddFacturas(facturas:Facturas):void{
      this.apiService.agregarFactura(facturas).subscribe(res=>{
        if(res){
          this.toastr.success("Factura agregada");
          this.clear();
        } else{
          alert("Error")
        }
      })
    }

    onSetData(select: any){
      this.c_clientes.idCliente = select.idCliente;
      this.c_clientes.nombre = select.nombre;
      this.c_clientes.apellido = select.apellido;
      this.c_clientes.telefono = select.telefono;
      this.c_clientes.cedula = select.cedula;
      this.c_facturas.clienteId = select.idCliente;
      this.c_facturas.pagoNuevo = 0;
      this.c_facturas.pagoRealizado = 0;
    }

    //Limpieza 
    clear(){
      this.c_facturas.montoSolicitado = 0;
      this.c_facturas.tasa = 0;
      this.c_facturas.cuotas = 0;
      this.c_facturas.cuotasMensuales = 0;
      this.c_facturas.capital = 0;
      this.c_facturas.interes = 0;
      this.c_facturas.clienteId = 0;
    }

    /*clearMontoPagar(){
      this.h_facturas.FACTURA_ID = 0;
      this.h_facturas.CLIENTE_ID = 0;
      this.h_facturas.NOMBRE_APELLIDO = "";
      this.h_facturas.TELEFONO = "";
      this.h_facturas.CEDULA = "";
      this.h_facturas.MONTO_SOLICITADO = 0;
      this.h_facturas.PAGO_REALIZADO = 0;
      this.h_facturas.TASA = 0;
      this.h_facturas.CUOTAS = 0;
      this.h_facturas.CUOTAS_MENSUALES = 0;
      this.h_facturas.CAPITAL = 0;
      this.h_facturas.INTERES = 0;
      this.h_facturas.FECHA  = new Date;
      //Actualizando factura
      this.u_facturas.ID_FACTURAS = this.h_facturas.FACTURA_ID;
      this.u_facturas.CLIENTE_ID = this.h_facturas.CLIENTE_ID;
      this.u_facturas.NOMBRE_APELLIDO = this.h_facturas.NOMBRE_APELLIDO;
      this.u_facturas.TELEFONO = this.h_facturas.TELEFONO;
      this.u_facturas.CEDULA = this.h_facturas.CEDULA;
      this.u_facturas.MONTO_SOLICITADO = this.h_facturas.MONTO_SOLICITADO;
      this.u_facturas.PAGO_REALIZADO = this.h_facturas.PAGO_REALIZADO;
      this.u_facturas.TASA = this.h_facturas.TASA;
      this.u_facturas.CUOTAS = this.h_facturas.CUOTAS;
      this.u_facturas.CUOTAS_MENSUALES = this.h_facturas.CUOTAS_MENSUALES;
      this.u_facturas.CAPITAL = this.h_facturas.CAPITAL;
      this.u_facturas.INTERES = this.h_facturas.INTERES;
      this.u_facturas.FECHA = this.h_facturas.FECHA;
    }*/

    calculos(){
      this.c_facturas.cuotasMensuales = (this.c_facturas.montoSolicitado * this.c_facturas.tasa / this.c_facturas.cuotas);
      this.c_facturas.cuotasMensuales = Number(this.c_facturas.cuotasMensuales.toFixed(2));
      this.c_facturas.capital = (this.c_facturas.montoSolicitado / this.c_facturas.cuotas);
      this.c_facturas.capital = Number(this.c_facturas.capital.toFixed(2));
      this.c_facturas.interes = (this.c_facturas.cuotasMensuales - this.c_facturas.capital);
      this.c_facturas.interes = Number(this.c_facturas.interes.toFixed(2));
    }

    //Modal Perfil
    openFullscreen(content){
      this.modal.open(content, {fullscreen: true});
    }

    //Facturas
    ObserverChangeSearch2(){
      this.control.valueChanges
        .pipe(
          debounceTime(500)
        )
        .subscribe(query=>{
          console.log(query);
          this.apiService.getClientesFacturas(query).subscribe(
            (res:any) => {
              this.facturas = res;
            },
            (object) => {
              console.log(object)
            }
          )
        })
    }

    /*onSetData2(select: any){
      this.c_facturas.ID_FACTURAS = select.ID_FACTURAS;
      this.c_facturas.CLIENTE_ID = select.ID_CLIENTE;
      this.c_facturas.NOMBRE_APELLIDO = select.NOMBRE_APELLIDO;
      this.c_facturas.TELEFONO = select.TELEFONO;
      this.c_facturas.CEDULA = select.CEDULA;
      this.c_facturas.FECHA = select.FECHA;
    }*/

    /*Monto a Pagar*/
    onSetData3(select: any){
      //Creando historial
      this.c_clienteFactura.idCliente = select.idCliente;
      this.c_clienteFactura.nombre = select.nombre;
      this.c_clienteFactura.apellido = select.apellido;
      this.c_clienteFactura.cedula = select.cedula;
      this.c_clienteFactura.telefono = select.telefono;
      this.c_clienteFactura.idFactura = select.idFactura;
      this.c_clienteFactura.montoSolicitado = select.montoSolicitado;
      this.c_clienteFactura.tasa = select.tasa;
      this.c_clienteFactura.cuotas = select.cuotas;
      this.c_clienteFactura.cuotasMensuales = select.cuotasMensuales;
      this.c_clienteFactura.capital = select.capital;
      this.c_clienteFactura.interes = select.interes;
      this.c_clienteFactura.pagoNuevo = select.pagoNuevo;
      this.c_clienteFactura.pagoRealizado = select.pagoRealizado;
      this.c_clienteFactura.fecha = select.fecha;
      /*Historial*/
      this.c_historial.clienteId = this.c_clienteFactura.idCliente;
      this.c_historial.nombre = this.c_clienteFactura.nombre;
      this.c_historial.apellido = this.c_clienteFactura.apellido;
      this.c_historial.cedula = this.c_clienteFactura.cedula;
      this.c_historial.telefono = this.c_clienteFactura.telefono;
      this.c_historial.facturaId = this.c_clienteFactura.idFactura;
      this.c_historial.montoSolicitado = this.c_clienteFactura.montoSolicitado;
      this.c_historial.tasa = this.c_clienteFactura.tasa;
      this.c_historial.cuotas = this.c_clienteFactura.cuotas;
      this.c_historial.cuotasMensuales = this.c_clienteFactura.cuotasMensuales;
      this.c_historial.capital = this.c_clienteFactura.capital;
      this.c_historial.interes = this.c_clienteFactura.interes;
      this.c_historial.pagoNuevo = this.c_clienteFactura.pagoNuevo;
      this.c_historial.pagoRealizado = this.c_clienteFactura.pagoRealizado;
      this.c_historial.fecha = this.c_clienteFactura.fecha;
      /*Factura */
      this.u_factura.idFactura = select.idFactura;
      this.u_factura.montoSolicitado = select.montoSolicitado;
      this.u_factura.tasa = select.tasa;
      this.u_factura.cuotas = select.cuotas;
      this.u_factura.cuotasMensuales = select.cuotasMensuales;
      this.u_factura.capital = select.capital;
      this.u_factura.interes = select.interes;
      this.u_factura.pagoNuevo = select.pagoNuevo;
      this.u_factura.pagoRealizado = select.pagoRealizado;
      this.u_factura.clienteId = select.clienteId;
    }

    onAddHistorial(c_historial:HistorialCliente):void{
      this.apiService.agregarHistorial(c_historial).subscribe(res=>{
        if(res){
          this.toastr.success("Historial Agregado");
          //this.clear();
        } else {
          alert("Error")
        }
      })
    }

    onUpdateFactura(facturas:Facturas):void{
      this.apiService.updateFactura(facturas).subscribe(res=> {
        if(res){
          this.toastr.warning("Factura Actualizada");
          //this.clear();
        } else {
          alert("Error")
        }
      })
    }

    calcularMontoPagar(){
      this.c_clienteFactura.capital = (this.c_clienteFactura.pagoRealizado - this.c_clienteFactura.interes);
      this.c_clienteFactura.capital = Number(this.c_clienteFactura.capital.toFixed(2));
      //this.u_facturas.CAPITAL = (this.u_facturas.PAGO_REALIZADO - this.u_facturas.INTERES);

      this.c_clienteFactura.montoSolicitado = (this.c_clienteFactura.montoSolicitado - this.c_clienteFactura.capital);
      this.c_clienteFactura.montoSolicitado = Number(this.c_clienteFactura.montoSolicitado.toFixed(2));
      /*this.u_facturas.MONTO_SOLICITADO = (this.u_facturas.MONTO_SOLICITADO - this.h_facturas.CAPITAL);
      this.u_facturas.MONTO_SOLICITADO = Number(this.u_facturas.MONTO_SOLICITADO.toFixed(2));*/

      this.c_clienteFactura.capital = (this.c_clienteFactura.montoSolicitado / this.c_clienteFactura.cuotas);
      this.c_clienteFactura.capital = Number(this.c_clienteFactura.capital.toFixed(2));

      this.c_clienteFactura.cuotasMensuales = (this.c_clienteFactura.montoSolicitado * this.c_clienteFactura.tasa / this.c_clienteFactura.cuotas);
      this.c_clienteFactura.cuotasMensuales = Number(this.c_clienteFactura.cuotasMensuales.toFixed(2));
      /*this.u_facturas.CUOTAS_MENSUALES = (this.u_facturas.MONTO_SOLICITADO * this.u_facturas.TASA / this.u_facturas.CUOTAS);
      this.u_facturas.CUOTAS_MENSUALES = Number(this.u_facturas.CUOTAS_MENSUALES.toFixed(2));*/

      this.c_clienteFactura.interes = (this.c_clienteFactura.cuotasMensuales - this.c_clienteFactura.capital);
      this.c_clienteFactura.interes = Number(this.c_clienteFactura.interes.toFixed(2));
    }

   /* clearMontoPagar2(){
      this.u_facturas.PAGO_REALIZADO = 0;
      this.h_facturas.PAGO_REALIZADO = 0;
    }

    //Nuevo Prestamo
    onSetData4(select: any){
      this.h_facturas.FACTURA_ID = select.ID_FACTURAS;
      this.h_facturas.CLIENTE_ID = select.CLIENTE_ID;
      this.h_facturas.NOMBRE_APELLIDO = select.NOMBRE_APELLIDO;
      this.h_facturas.TELEFONO = select.TELEFONO;
      this.h_facturas.CEDULA = select.CEDULA;
      this.h_facturas.MONTO_SOLICITADO = select.MONTO_SOLICITADO;
      this.h_facturas.TASA = select.TASA;
      this.h_facturas.CUOTAS = select.CUOTAS;
      this.h_facturas.CUOTAS_MENSUALES = select.CUOTAS_MENSUALES;
      this.h_facturas.CAPITAL = select.CAPITAL;
      this.h_facturas.INTERES = select.INTERES;
      this.h_facturas.FECHA = select.FECHA;
      //Actualizando factura
      this.u_facturas.ID_FACTURAS = this.h_facturas.FACTURA_ID;
      this.u_facturas.CLIENTE_ID = this.h_facturas.CLIENTE_ID;
      this.u_facturas.NOMBRE_APELLIDO = this.h_facturas.NOMBRE_APELLIDO;
      this.u_facturas.TELEFONO = this.h_facturas.TELEFONO;
      this.u_facturas.CEDULA = this.h_facturas.CEDULA;
      this.u_facturas.MONTO_SOLICITADO = this.h_facturas.MONTO_SOLICITADO;
      this.u_facturas.TASA = this.h_facturas.TASA;
      this.u_facturas.CUOTAS = this.h_facturas.CUOTAS;
      this.u_facturas.CUOTAS_MENSUALES = this.h_facturas.CUOTAS_MENSUALES;
      this.u_facturas.CAPITAL = this.h_facturas.CAPITAL;
      this.u_facturas.INTERES = this.h_facturas.INTERES;
      this.u_facturas.FECHA = this.h_facturas.FECHA;
    }

    nuevoPrestamo(){
      //this.u_facturas.INTERES = Number(this.u_facturas.INTERES.toFixed(2));
      this.h_facturas.MONTO_SOLICITADO = (this.h_facturas.MONTO_SOLICITADO + this.h_facturas.PAGO_NUEVO);
      this.h_facturas.MONTO_SOLICITADO = Number(this.h_facturas.MONTO_SOLICITADO.toFixed(2));
      this.u_facturas.MONTO_SOLICITADO = (this.u_facturas.MONTO_SOLICITADO + this.h_facturas.PAGO_NUEVO);
      this.u_facturas.MONTO_SOLICITADO = Number(this.u_facturas.MONTO_SOLICITADO.toFixed(2));

      this.u_facturas.CUOTAS_MENSUALES = (this.u_facturas.MONTO_SOLICITADO * this.u_facturas.TASA / this.u_facturas.CUOTAS);
      this.u_facturas.CUOTAS_MENSUALES = Number(this.u_facturas.CUOTAS_MENSUALES.toFixed(2));
      this.u_facturas.CAPITAL = (this.u_facturas.MONTO_SOLICITADO/this.u_facturas.CUOTAS);
      this.u_facturas.CAPITAL = Number(this.u_facturas.CAPITAL.toFixed(2));
      this.u_facturas.INTERES = (this.u_facturas.CUOTAS_MENSUALES - this.u_facturas.CAPITAL);
      this.u_facturas.INTERES = Number(this.u_facturas.INTERES.toFixed(2));

      this.h_facturas.CAPITAL = 0;
      this.h_facturas.INTERES = 0;
    }*/

    ObserverChangeSearch3(){
      this.control.valueChanges
        .pipe(
          debounceTime(500)
        )
        .subscribe(query=>{
          console.log(query);
          this.apiService.getHistorial(query).subscribe(
            (res:any) => {
              this.historial = res;
            },
            (object) => {
              console.log(object)
            }
          )
        })
    }

    /*validate(event: any){
      var t = event.target.value;
      event.target.value = 
        t.indexOf(',') >= 0
          ? t.substr(0, t.indexOf(','))+t.substr(t.indexOf(','),2)
            : t;
    }*/
}
