﻿using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Gestor_Productos.Database.ORM_GestProc
{

    public partial class USUARIOS
    {
        public USUARIOS(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}