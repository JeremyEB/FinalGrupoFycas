import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounce, debounceTime } from 'rxjs';
import { Facturas } from 'src/app/models/Modelos';
import { ApiService } from 'src/app/services/api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-clientes-vencidos',
  templateUrl: './clientes-vencidos.component.html',
  styleUrls: ['./clientes-vencidos.component.css']
})
export class ClientesVencidosComponent implements OnInit{
  u_factura: Facturas = new Facturas();
  dataTable: any = [];
  control = new FormControl();

  constructor(
    private apiService: ApiService,
    private toastr: ToastrService
  ){ }
  
  ngOnInit(): void {
      this.ObserverChangeSearch();
  }

  onSetData(select:any){
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

  ObserverChangeSearch(){
    this.control.valueChanges
      .pipe(
        debounceTime(500)
      )
      .subscribe(query=> {
        console.log(query);
        this.apiService.getFacturas(query).subscribe(
          (res:any) => {
            this.dataTable = res;
          },
          (object) => {
            console.log(object)
          }
        )
      })
  }

  onUpdate(facturas:Facturas):void{
    this.apiService.updateFactura(facturas).subscribe(res=> {
      if(res){
        this.toastr.warning("Factura Actualizada");
        //this.clear();
      } else {
        alert("Error")
      }
    })
  }

}
