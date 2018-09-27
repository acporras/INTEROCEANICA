using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using FinalXML.Entidades;

namespace FinalXML.Interfaces
{
    interface IEmpresa
    {
        DataTable CargaEmpresa();
        Contribuyente LeerEmpresa(String NumRuc);
    }
}
