using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using FinalXML.Entidades;

namespace FinalXML.Interfaces
{
    interface ICargaVentas
    {
        Boolean Update(clsCargaVentas ven);
        clsCargaVentas LeerVenta(String Sigla, String Serie, String Numeracion);
        List<DetalleDocumento> LeerVentaDetalle(String Sigla, String Serie, String Numeracion);
        DataTable CargaVentas(DateTime desde, DateTime hasta);
        DataTable LeerDetalle(String Sigla, String Serie, String Numeracion);
    }
}
