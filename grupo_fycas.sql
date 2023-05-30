-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 30, 2023 at 05:07 AM
-- Server version: 10.4.20-MariaDB
-- PHP Version: 8.0.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `grupo_fycas`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_editar_cliente` (`idcliente` INT, `nombre` VARCHAR(25), `apellido` VARCHAR(50), `cedula` VARCHAR(13), `telefono` VARCHAR(12))  BEGIN
	UPDATE clientes SET 
    NOMBRE = nombre,
    APELLIDO = apellido,
    CEDULA = cedula,
    TELEFONO = telefono
    WHERE ID_CLIENTE = idcliente;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_editar_factura` (`idfactura` INT, `montosolicitado` DECIMAL(9,2), `tasa` DECIMAL(5,2), `cuotas` DECIMAL(9,2), `cuotasmensuales` DECIMAL(9,2), `capital` DECIMAL(9,2), `interes` DECIMAL(9,2), `pagonuevo` DECIMAL(9,2), `pagorealizado` DECIMAL(9,2), `fecha` DATETIME, `clienteid` INT)  BEGIN
	UPDATE facturas SET
    MONTO_SOLICITADO  = montosolicitado,
    TASA = tasa,
    CUOTAS = cuotas,
    CUOTAS_MENSUALES = cuotasmensuales,
    CAPITAL = capital,
    INTERES = interes,
    PAGO_NUEVO = pagonuevo,
    PAGO_REALIZADO = pagorealizado,
    FECHA = fecha,
    CLIENTE_ID = clienteid
    Where ID_FACTURA = idfactura;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_eliminar_clientes` (`idcliente` INT)  BEGIN
	DELETE FROM clientes WHERE ID_CLIENTE = idcliente;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_guardar_cliente` (`nombre` VARCHAR(25), `apellido` VARCHAR(50), `cedula` VARCHAR(13), `telefono` VARCHAR(12))  BEGIN
	INSERT INTO clientes (NOMBRE, APELLIDO, CEDULA, TELEFONO) VALUES (nombre, apellido, cedula, telefono);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_guardar_factura` (`montosolicitado` DECIMAL(9,2), `tasa` DECIMAL(5,2), `cuotas` DECIMAL(9,2), `cuotasmensuales` DECIMAL(9,2), `capital` DECIMAL(9,2), `interes` DECIMAL(9,2), `pagonuevo` DECIMAL(9,2), `pagorealizado` DECIMAL(9,2), `fecha` DATETIME, `clienteid` INT)  BEGIN
	INSERT INTO facturas (MONTO_SOLICITADO, TASA, CUOTAS, CUOTAS_MENSUALES, CAPITAL, INTERES, PAGO_NUEVO, PAGO_REALIZADO, FECHA, CLIENTE_ID)
	VALUES
    (
	montosolicitado,
    tasa,
    cuotas,
    cuotasmensuales,
    capital,
    interes,
    pagonuevo,
    pagorealizado,
    fecha,
    clienteid
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_guardar_historialfactura` (`clienteid` INT, `nombre` VARCHAR(25), `apellido` VARCHAR(50), `cedula` VARCHAR(13), `telefono` VARCHAR(12), `facturaid` INT, `montosolicitado` DECIMAL(9,2), `tasa` DECIMAL(5,2), `cuotas` DECIMAL(9,2), `cuotasmensuales` DECIMAL(9,2), `capital` DECIMAL(9,2), `interes` DECIMAL(9,2), `pagonuevo` DECIMAL(9,2), `pagorealizado` DECIMAL(9,2), `fecha` DATETIME)  BEGIN
	INSERT INTO historialfacturas (
    CLIENTE_ID,
    NOMBRE, 
    APELLIDO, 
    CEDULA, 
    TELEFONO,
    FACTURA_ID,
    MONTO_SOLICITADO, 
    TASA, 
    CUOTAS, 
    CUOTAS_MENSUALES, 
    CAPITAL, 
    INTERES, 
    PAGO_NUEVO, 
    PAGO_REALIZADO, 
    FECHA)
    VALUES (
    clienteid,
	nombre, 
    apellido, 
    cedula, 
    telefono,
    facturaid,
    montosolicitado,
    tasa,
    cuotas,
    cuotasmensuales,
    capital,
    interes,
    pagonuevo,
    pagorealizado,
    fecha
    );
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_lista_clientes` ()  BEGIN
	SELECT * FROM CLIENTES;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_lista_clientes_facturas` ()  BEGIN
	SELECT * FROM clientes INNER JOIN facturas ON clientes.ID_CLIENTE = facturas.CLIENTE_ID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_lista_facturas` ()  BEGIN
	SELECT * FROM facturas;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_lista_historialFactura` ()  BEGIN
	SELECT * FROM historialfacturas;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_obtener_cliente` (`c_cedula` VARCHAR(13))  BEGIN
	SELECT ID_CLIENTE, NOMBRE, APELLIDO, CEDULA, TELEFONO FROM clientes where CEDULA = c_cedula;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `clientes`
--

CREATE TABLE `clientes` (
  `ID_CLIENTE` int(11) NOT NULL,
  `NOMBRE` varchar(25) DEFAULT NULL,
  `APELLIDO` varchar(50) DEFAULT NULL,
  `CEDULA` varchar(13) DEFAULT NULL,
  `TELEFONO` varchar(12) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `clientes`
--

INSERT INTO `clientes` (`ID_CLIENTE`, `NOMBRE`, `APELLIDO`, `CEDULA`, `TELEFONO`) VALUES
(1, 'Jeremy', 'Encarnacion Basilio', '402-2970588-0', '829-722-3118'),
(2, 'Anthony Beltre', 'Vega Feliz', '402-2970588-1', '809-000-0000'),
(4, 'Heriz', 'Castillo', '402-2970588-4', '809-568-0000'),
(12, 'Nate', 'Jacobs', '402-2970588-6', '809-568-3333');

-- --------------------------------------------------------

--
-- Table structure for table `facturas`
--

CREATE TABLE `facturas` (
  `ID_FACTURA` int(11) NOT NULL,
  `MONTO_SOLICITADO` decimal(9,2) DEFAULT NULL,
  `TASA` decimal(5,2) DEFAULT NULL,
  `CUOTAS` decimal(9,2) DEFAULT NULL,
  `CUOTAS_MENSUALES` decimal(9,2) DEFAULT NULL,
  `CAPITAL` decimal(9,2) DEFAULT NULL,
  `INTERES` decimal(9,2) DEFAULT NULL,
  `PAGO_NUEVO` decimal(9,2) DEFAULT NULL,
  `PAGO_REALIZADO` decimal(9,2) DEFAULT NULL,
  `FECHA` datetime DEFAULT NULL,
  `CLIENTE_ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `facturas`
--

INSERT INTO `facturas` (`ID_FACTURA`, `MONTO_SOLICITADO`, `TASA`, `CUOTAS`, `CUOTAS_MENSUALES`, `CAPITAL`, `INTERES`, `PAGO_NUEVO`, `PAGO_REALIZADO`, `FECHA`, `CLIENTE_ID`) VALUES
(1, '25000.00', '1.58', '6.00', '5000.00', '4300.00', '4500.00', '5000.00', '3300.00', '2023-05-23 00:00:00', 1),
(5, '35000.00', '1.54', '6.00', '7500.00', '7000.00', '6500.00', '6000.00', '5000.00', '2023-05-27 00:00:00', 1),
(7, '10000.00', '1.15', '6.00', '2300.00', '2000.00', '300.00', '0.00', '0.00', '2023-05-27 00:00:00', 2),
(10, '50000.00', '1.58', '6.00', '13166.00', '8333.00', '4833.00', '0.00', '0.00', '2023-05-29 00:00:00', 12);

-- --------------------------------------------------------

--
-- Table structure for table `historialfacturas`
--

CREATE TABLE `historialfacturas` (
  `ID_HISTORIALFACTURA` int(11) NOT NULL,
  `CLIENTE_ID` int(11) DEFAULT NULL,
  `NOMBRE` varchar(25) DEFAULT NULL,
  `APELLIDO` varchar(50) DEFAULT NULL,
  `CEDULA` varchar(13) DEFAULT NULL,
  `TELEFONO` varchar(12) DEFAULT NULL,
  `FACTURA_ID` int(11) DEFAULT NULL,
  `MONTO_SOLICITADO` decimal(9,2) DEFAULT NULL,
  `TASA` decimal(5,2) DEFAULT NULL,
  `CUOTAS` decimal(9,2) DEFAULT NULL,
  `CUOTAS_MENSUALES` decimal(9,2) DEFAULT NULL,
  `CAPITAL` decimal(9,2) DEFAULT NULL,
  `INTERES` decimal(9,2) DEFAULT NULL,
  `PAGO_NUEVO` decimal(9,2) DEFAULT NULL,
  `PAGO_REALIZADO` decimal(9,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `historialfacturas`
--

INSERT INTO `historialfacturas` (`ID_HISTORIALFACTURA`, `CLIENTE_ID`, `NOMBRE`, `APELLIDO`, `CEDULA`, `TELEFONO`, `FACTURA_ID`, `MONTO_SOLICITADO`, `TASA`, `CUOTAS`, `CUOTAS_MENSUALES`, `CAPITAL`, `INTERES`, `PAGO_NUEVO`, `PAGO_REALIZADO`, `fecha`) VALUES
(2, 2, 'Anthony Beltre', 'Vega Feliz', '402-2970588-1', '809-000-0000', 7, '10000.00', '1.15', '5.00', '2300.00', '2000.00', '300.00', '0.00', '0.00', '2023-05-27 00:00:00'),
(6, 2, 'Anthony Beltre', 'Vega Feliz', '402-2970588-1', '809-000-0000', 7, '10000.00', '1.15', '4.00', '2300.00', '2000.00', '300.00', '0.00', '0.00', '2023-05-27 00:00:00'),
(7, 2, 'Anthony Beltre', 'Vega Feliz', '402-2970588-1', '809-000-0000', 7, '10000.00', '1.15', '6.00', '2300.00', '2000.00', '300.00', '0.00', '5000.00', '2023-05-27 00:00:00'),
(8, 12, 'Nate', 'Jacobs', '402-2970588-6', '809-568-3333', 8, '100000.00', '1.28', '9.00', '12800.00', '8889.00', '2489.00', '0.00', '12800.00', '2023-05-30 00:00:00'),
(9, 12, 'Nate', 'Jacobs', '402-2970588-6', '809-568-3333', 9, '100000.00', '1.25', '5.00', '24600.00', '20000.00', '4600.00', '0.00', '15000.00', '2023-05-29 01:15:02');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`ID_CLIENTE`);

--
-- Indexes for table `facturas`
--
ALTER TABLE `facturas`
  ADD PRIMARY KEY (`ID_FACTURA`),
  ADD KEY `CLIENTE_ID` (`CLIENTE_ID`);

--
-- Indexes for table `historialfacturas`
--
ALTER TABLE `historialfacturas`
  ADD PRIMARY KEY (`ID_HISTORIALFACTURA`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clientes`
--
ALTER TABLE `clientes`
  MODIFY `ID_CLIENTE` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `facturas`
--
ALTER TABLE `facturas`
  MODIFY `ID_FACTURA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `historialfacturas`
--
ALTER TABLE `historialfacturas`
  MODIFY `ID_HISTORIALFACTURA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `facturas`
--
ALTER TABLE `facturas`
  ADD CONSTRAINT `facturas_ibfk_1` FOREIGN KEY (`CLIENTE_ID`) REFERENCES `clientes` (`ID_CLIENTE`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
