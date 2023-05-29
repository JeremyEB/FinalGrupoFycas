import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, timeout } from 'rxjs';
import { Clientes } from 'src/app/models/Modelos';
import { ApiService } from 'src/app/services/api.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  c_clientes: Clientes = new Clientes();
  dataTable: any = [];
  dataTable2: any = [];
  control = new  FormControl();
  public clientes: Array<any> = []

  constructor( 
    private apiService: ApiService,
    private toastr: ToastrService,
    public modal: NgbModal,
    public modal2: NgbModal
  ) { }

  ngOnInit(): void {
    this.onDataTable();
    this.ObserverChangeSearch();
  }

  onDataTable(){
    this.apiService.getAllClientes().subscribe(res=>{
      this.dataTable = res;
        console.log(res);
    })
  }

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

  onAddClientes(clientes:Clientes):void{
    this.apiService.agregarCliente(clientes).subscribe(res=>{
      if(res){
        this.toastr.success('Cliente Agregado Satisfactoriamente');
        this.clear();
      } else {
        alert("Error")
      }
    })
  }

  onUpdateClientes(clientes:Clientes):void{
    this.apiService.updateClientes(clientes).subscribe(res=>{
      if(res){
        this.toastr.warning("Cliente actualizado");
        this.clear();
      } else {
        alert("Error")
      }
    })
  }

    onSetData(select:any){
      this.c_clientes.idCliente = select.idCliente;
      this.c_clientes.nombre = select.nombre;
      this.c_clientes.apellido = select.apellido;
      this.c_clientes.cedula = select.cedula;
      this.c_clientes.telefono = select.telefono;
    }

    clear(){
      this.c_clientes.nombre = "";
      this.c_clientes.apellido = "";
      this.c_clientes.cedula = "";
      this.c_clientes.telefono = "";
    }

    //Modal Cliente Crud
    openFullscreen(content){
      this.modal.open(content, {fullscreen: true});
    }

    eliminarCliente(){
      this.toastr.error("Contactar con el programador")
    }
}
