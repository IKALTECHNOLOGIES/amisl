﻿<%@ Page Title="Etiquetas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Etiqueta.aspx.cs" Inherits="ONG._Etiqueta" %>


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
        <style>
            div.filtering {
                border: 1px solid #999;
                margin-bottom: 5px;
                padding: 10px;
                background-color: #EEE;
            }
        </style>

    </head>
    <body>
        <div class="site-container">
            <h2>Etiquetas</h2>
            <div class="filtering">
                <form>
                <input type="text" name="search" id="search" />
                    <button type="submit" id="SearchButton">Buscar</button>
                </form>
            </div>
            <div id="EtiquetaTable"></div>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#EtiquetaTable').jtable({
                    title: 'Lista Etiquetas',
                    paging: true,
                    pageSize: 10,
                    sorting: true,
                    defaultSorting: 'VALOR ASC',
                    actions: {
                        listAction: '/Etiqueta.aspx/ObtieneListaEtiqueta',
                        createAction: '/Etiqueta.aspx/Create',
                        updateAction: '/Etiqueta.aspx/Edit',
                        deleteAction: '/Etiqueta.aspx/Delete'
                    },
                    fields: {
                        UUID: {
                            key: true,
                            list: false
                        },
                        VALOR: {
                            title: 'Valor',
                            width: '25%'
                        },
                        UUID_IDIOMA: {
                            title: 'Idioma',
                            width: '25%',
                            options: '/Etiqueta.aspx/ObtieneOpcionesIdioma'
                        },
                        UUID_REGISTRO: {
                            title: 'Registro',
                            width: '25%'
                        },
                        IDENTIFICADOR: {
                            title: 'Identificador',
                            width: '15%'
                        },
                        COMENTARIOS: {
                            title: 'Comentarios',
                            type: 'textarea',
                            list: false
                        }
                    },
                    //Initialize validation logic when a form is created
                    formCreated: function (event, data) {
                        data.form.find('input[name="VALOR"]').addClass('validate[required]');
                        data.form.find('input[name="IDENTIFICADOR"]').addClass('validate[required]');
                        data.form.find('input[name="UUID_REGISTRO"]').addClass('validate[required]');
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
                //$('#EtiquetaTable').jtable('reload');
                //Re-load records when user click 'load records' button.
                $('#SearchButton').click(function (e) {
                    e.preventDefault();
                    $('#EtiquetaTable').jtable('load', {
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

