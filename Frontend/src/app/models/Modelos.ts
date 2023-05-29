export class Clientes {
    idCliente: number = 0;
    nombre: string = "";
    apellido: string = "";
    cedula: string = "";
    telefono: string = "";
}

export class Facturas {
    idFactura: number = 0;
    montoSolicitado: number = 0;
    tasa: number;
    cuotas: number;
    cuotasMensuales:number;
    capital:number;
    interes:number;
    pagoNuevo:number;
    pagoRealizado:number;
    fecha:Date = new Date();
    clienteId:number;
}

export class ClientesFacturas {
    idCliente: number = 0;
    nombre: string = "";
    apellido: string = "";
    cedula: string = "";
    telefono: string = "";
    idFactura: number = 0;
    montoSolicitado: number = 0;
    tasa: number;
    cuotas: number;
    cuotasMensuales:number;
    capital:number;
    interes:number;
    pagoNuevo:number;
    pagoRealizado:number;
    fecha:Date = new Date();
    clienteId:number;
}

export class HistorialCliente{
    idHistorialfactura: number = 0;
    clienteId: number = 0;
    nombre: string = "";
    apellido: string = "";
    cedula: string = "";
    telefono: string = "";
    facturaId: number = 0;
    montoSolicitado: number = 0;
    tasa: number = 0;
    cuotas: number = 0;
    cuotasMensuales:number = 0;
    capital:number = 0;
    interes:number = 0;
    pagoNuevo:number = 0;
    pagoRealizado:number = 0;
    fecha:Date = new Date();
}