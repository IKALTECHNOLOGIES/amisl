<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ONG.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    
<head runat="server">
    <title></title>
    <link href="/Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="/Content/themes/metroblue/jquery-ui.css" rel="stylesheet" type="text/css" />

    <!-- jTable style file -->
    <link href="/Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet" type="text/css" />

    <script src="/Scripts/modernizr-2.6.2.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.9.2.min.js" type="text/javascript"></script>

    <script src="/Scripts/jtablesite.js" type="text/javascript"></script>

    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="/Scripts/jtable/external/json2.js"></script>
    <!-- Core jTable script file -->
    <script type="text/javascript" src="/Scripts/jtable/jquery.jtable.js"></script>
    <!-- ASP.NET Web Forms extension for jTable -->
    <script type="text/javascript" src="/Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>

</head>
<body>
    <div class="site-container">
    <h2>Prueba</h2>
    <div id="NivelTable"></div>
        </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#NivelTable').jtable({
                title: 'Lista Niveles',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'NIVEL ASC',
                actions: {
                    listAction: '/WebForm1.aspx/ObtieneListaNivel',
                    createAction: '/WebForm1.aspx/Create',
                    updateAction: '/WebForm1.aspx/Edit',
                    deleteAction: '/WebForm1.aspx/Delete'
                },
                fields: {
                    UUID: {
                        key: true,
                        list: false
                    },
                    NIVEL1: {
                        title: 'Nivel',
                        width: '20%'
                    },
                    IDENTIFICADOR: {
                        title: 'Identificador',
                        width: '20%'
                    },
                    SUPERIOR: {
                        title: 'Superior',
                        width: '20%',
                    }
                }
            });
            $('#NivelTable').jtable('reload');
        });
     </script>
</body>
</html>
