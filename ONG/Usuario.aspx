<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ONG._Usuario" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    

   <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    
<head>
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
    <script type="text/javascript" src="/Scripts/jtable/localization/jquery.jtable.es.js"></script>
    
    <!-- Import CSS file for validation engine (in Head section of HTML) -->
    <link href="/Scripts/validationEngine/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <!-- Import Javascript files for validation engine (in Head section of HTML) -->
    <script type="text/javascript" src="/Scripts/validationEngine/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="/Scripts/validationEngine/languages/jquery.validationEngine-es.js"></script>

    
</head>
<body>
    <div class="site-container">
    <h2>Usuarios</h2>
        <div class="filtering">
                <form>
                    <input type="text" name="search" id="search" />
                    <button type="submit" id="SearchButton">Buscar</button>
                </form>
            </div>
    <div id="UsuarioTable"></div>
        </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#UsuarioTable').jtable({
                title: 'Lista Usuarios',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'NOMBRE ASC',
                actions: {
                    listAction: '/Usuario.aspx/ObtieneListaUsuario',
                    createAction: '/Usuario.aspx/Create',
                    updateAction: '/Usuario.aspx/Edit',
                    deleteAction: '/Usuario.aspx/Delete'
                },
                fields: {
                    UUID: {
                        key: true,
                        list: false
                    },
                    NOMBRE: {
                        title: 'Nombre',
                        width: '20%'
                    },
                    APELLIDOS: {
                        title: 'Apellidos',
                        width: '20%'
                    },
                    USUARIO1: {
                        title: 'Usuario',
                        width: '10%'
                    },
                    TEL1: {
                        title: 'Teléfono 1',
                        width: '10%'
                    },
                    TEL2: {
                        title: 'Teléfono 2',
                        width: '10%',
                        list: false
                    },
                    TEL3: {
                        title: 'Teléfono 3',
                        width: '10%',
                        list: false
                    },
                    EMAIL: {
                        title: 'Email',
                        width: '15%'
                    },
                    DIR1: {
                        title: 'Dirección 1',
                        width: '15%'
                    },
                    DIR2: {
                        title: 'Dirección 2',
                        width: '15%',
                        list: false
                    },
                    DIR3: {
                        title: 'Dirección 3',
                        width: '15%',
                        list: false
                    },
                    NIVEL: {
                        title: 'Nivel',
                        width: '15%',
                        options: '/Usuario.aspx/ObtieneOpcionesNivel'
                    },
                    CLAVE: {
                        title: 'Clave',
                        type: 'password', 
                        width: '10%',
                        list: false
                    }
                },
                //Initialize validation logic when a form is created
                formCreated: function (event, data) {
                    data.form.find('input[name="NOMBRES"]').addClass('validate[required]');
                    data.form.find('input[name="APELLIDOS"]').addClass('validate[required]');
                    data.form.find('input[name="USUARIO1"]').addClass('validate[required]');
                    data.form.find('input[name="TEL1"]').addClass('validate[required]');
                    data.form.find('input[name="EMAIL"]').addClass('validate[required,custom[email]]');
                    data.form.find('input[name="DIR1"]').addClass('validate[required]');
                    data.form.find('input[name="NIVEL"]').addClass('validate[required]');
                    data.form.find('input[name="CLAVE"]').addClass('validate[required]');
                    data.form.validationEngine();
                },
                //Validate form when it is being submitted
                formSubmitting: function (event, data) {
                    return data.form.validationEngine('validate');
                },
                //Dispose validation logic when form is closed
                formClosed: function (event, data) {
                    data.form.validationEngine('hide');
                    data.form.validationEngine('detach');
                }
            });
            //$('#UsuarioTable').jtable('reload');
            //Re-load records when user click 'load records' button.
            $('#SearchButton').click(function (e) {
                e.preventDefault();
                $('#UsuarioTable').jtable('load', {
                    search: $('#search').val()
                });
            });
            //Load all records when page is first shown
            $('#SearchButton').click();
        });
     </script>
</body>
</html>
    
    
</asp:Content>

