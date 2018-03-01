<%@ Page Title="Reporte Financiero" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresoRepFinanciero.aspx.cs" Inherits="ONG._IngresoRepFinanciero" %>


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
            <h2>Reportes Financieros</h2>
            <div class="filtering">
                <form>
                    <input type="text" name="search" id="search" />
                    <button type="submit" id="SearchButton">Buscar</button>
                </form>
            </div>
            <div id="IngresoRepFinanciero"></div>

            <div id="dialog" style="display: none">
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#IngresoRepFinanciero').jtable({
                    title: 'Lista Reportes Financieros',
                    paging: true,
                    pageSize: 10,
                    sorting: true,
                    defaultSorting: 'FECHA DESC',
                    columnResizable: true,
                    openChildAsAccordion: true,
                    toolbar: {
                        hoverAnimation: true, //Enable/disable small animation on mouse hover to a toolbar item.
                        hoverAnimationDuration: 60, //Duration of the hover animation.
                        hoverAnimationEasing: undefined, //Easing of the hover animation. Uses jQuery's default animation ('swing') if set to undefined.

                    },
                    recordAdded: function (event, data) {
                        $('#IngresoRepFinanciero').jtable('reload');
                    },
                    actions: {
                        listAction: '/IngresoRepFinanciero.aspx/ObtieneListaIngresoRep',
                        createAction: '/IngresoRepFinanciero.aspx/Create',
                        updateAction: '/IngresoRepFinanciero.aspx/Edit',
                        deleteAction: '/IngresoRepFinanciero.aspx/Delete'
                    },
                    fields: {
                        UUID: {
                            key: true,
                            list: false
                        },
                        //TABLA HIJA PARA INGRESOS DEL REPORTE FINANCIERO"
                        INGRESOS: {
                            title: '',
                            width: '1%',
                            sorting: false,
                            edit: false,
                            create: false,
                            display: function (reporteData) {
                                //Create an image that will be used to open child table
                                var $img = $('<img src="/Content/images/income.png" title="Registrar Ingresos" />');
                                //Open child table when user clicks the image
                                $img.click(function () {
                                    $('#IngresoRepFinanciero').jtable('openChildTable',
                                            $img.closest('tr'),
                                            {
                                                title: 'Reporte ' + reporteData.record.MES + ' - ' + reporteData.record.ANIO + ' - Ingresos',
                                                actions: {
                                                    listAction: '/IngresoRepFinanciero.aspx/ListIngreso?UUID_REP_FINANCIERO=' + reporteData.record.UUID,
                                                    deleteAction: '/IngresoRepFinanciero.aspx/DeleteIngreso',
                                                    updateAction: '/IngresoRepFinanciero.aspx/EditIngreso',
                                                    createAction: '/IngresoRepFinanciero.aspx/CreateIngreso'
                                                },
                                                recordAdded: function (event, data) {
                                                    $('.jtable-child-table-container').jtable('reload');
                                                },
                                                recordsLoaded: function (event, data) {
                                                //    var footer = $('table.jtable').find('tfoot');
                                                //    if (!footer.length) {
                                                //        footer = $('<tfoot>').appendTo('table.jtable');
                                                //        footer.append($('<td id="count">00</td><td id="sum" colspan="6">0.00</td>'));
                                                //    }
                                                //    var count = 0;
                                                //    var total = 0;
                                                //    var items = data.serverResponse['Records'];
                                                //    $.each(items, function (index, value) {
                                                //        count += 1;
                                                //        total += parseFloat(value.MONTO);
                                                //    });
                                                //    $('#count').html(count);
                                                //    $('#sum').html(total);
                                                //    //$('#sum').html('$' + total);
                                                //    $('table.jtable').append('<tfoot><tr><td colspan="6">TOTAL INGRESOS ' + data.serverResponse.total + '</td></tr></tfoot>');
                                                },
                                                fields: {
                                                    UUID: {
                                                        key: true,
                                                        create: false,
                                                        edit: false,
                                                        list: false
                                                    },
                                                    UUID_REP_FINANCIERO: {
                                                        type: 'hidden',
                                                        defaultValue: reporteData.record.UUID
                                                    },
                                                    UUID_INGRESO: {
                                                        title: 'Ingreso',
                                                        width: '30%',
                                                        options: '/IngresoRepFinanciero.aspx/ObtieneOpcionesIngreso'
                                                    },
                                                    MONTO: {
                                                        title: 'Monto',
                                                        width: '30%'
                                                    },
                                                    MONTODOLARES: {
                                                        title: 'US Dolar',
                                                        width: '15%',
                                                        create: false
                                                    }
                                                }
                                            }, function (data) { //opened handler
                                                data.childTable.jtable('load');
                                            });
                                });
                                //Return image to show on the person row
                                return $img;
                            }
                        },
                        //TABLA HIJA PARA GASTOS DEL REPORTE FINANCIERO"
                        GASTOS: {
                            title: '',
                            width: '1%',
                            sorting: false,
                            edit: false,
                            create: false,
                            display: function (reporteData) {
                                //Create an image that will be used to open child table
                                var $img = $('<img src="/Content/images/expense.png" title="Registar Gastos" />');
                                //Open child table when user clicks the image
                                $img.click(function () {
                                    $('#IngresoRepFinanciero').jtable('openChildTable',
                                            $img.closest('tr'),
                                            {
                                                title: 'Reporte ' + reporteData.record.MES + ' - ' + reporteData.record.ANIO + ' - Gastos',
                                                actions: {
                                                    listAction: '/IngresoRepFinanciero.aspx/ListGasto?UUID_REP_FINANCIERO=' + reporteData.record.UUID,
                                                    deleteAction: '/IngresoRepFinanciero.aspx/DeleteGasto',
                                                    updateAction: '/IngresoRepFinanciero.aspx/EditGasto',
                                                    createAction: '/IngresoRepFinanciero.aspx/CreateGasto'
                                                },
                                                recordAdded: function (event, data) {
                                                    $('.jtable-child-table-container').jtable('reload');
                                                },
                                                fields: {
                                                    UUID_REP_FINANCIERO: {
                                                        type: 'hidden',
                                                        defaultValue: reporteData.record.UUID
                                                    },
                                                    UUID: {
                                                        key: true,
                                                        create: false,
                                                        edit: false,
                                                        list: false
                                                    },
                                                    UUID_GASTO: {
                                                        title: 'Gasto',
                                                        width: '30%',
                                                        options: '/IngresoRepFinanciero.aspx/ObtieneOpcionesGasto'
                                                    },
                                                    MONTO: {
                                                        title: 'Monto',
                                                        width: '30%'
                                                    },
                                                    MONTODOLARES: {
                                                        title: 'US Dolar',
                                                        width: '15%',
                                                        create: false
                                                    }

                                                }
                                            }, function (data) { //opened handler
                                                data.childTable.jtable('load');
                                            });
                                });
                                //Return image to show on the person row
                                return $img;
                            }
                        },
                        //TABLA HIJA PARA ESTADISTICAS DEL REPORTE FINANCIERO"
                        ESTADISTICAS: {
                            title: '',
                            width: '1%',
                            sorting: false,
                            edit: false,
                            create: false,
                            display: function (reporteData) {
                                //Create an image that will be used to open child table
                                var $img = $('<img src="/Content/images/statistics.png" title="Registar Estadisticas" />');
                                //Open child table when user clicks the image
                                $img.click(function (e) {
                                    $('#IngresoRepFinanciero').jtable('openChildTable',
                                            $img.closest('tr'),
                                            {
                                                title: 'Reporte ' + reporteData.record.MES + ' - ' + reporteData.record.ANIO + ' - Estadisticas',
                                                actions: {
                                                    listAction: '/IngresoRepFinanciero.aspx/ListEstadistica?UUID_REP_FINANCIERO=' + reporteData.record.UUID,
                                                    deleteAction: '/IngresoRepFinanciero.aspx/DeleteEstadistica',
                                                    updateAction: '/IngresoRepFinanciero.aspx/EditEstadistica',
                                                    createAction: '/IngresoRepFinanciero.aspx/CreateEstadistica'
                                                },
                                                recordAdded: function (event, data) {
                                                    $('.jtable-child-table-container').jtable('reload');
                                                },
                                                fields: {
                                                    UUID_REP_FINANCIERO: {
                                                        type: 'hidden',
                                                        defaultValue: reporteData.record.UUID
                                                    },
                                                    UUID: {
                                                        key: true,
                                                        create: false,
                                                        edit: false,
                                                        list: false
                                                    },
                                                    UUID_ESTADISTICA: {
                                                        title: 'Estadística',
                                                        width: '30%',
                                                        options: '/IngresoRepFinanciero.aspx/ObtieneOpcionesEstadistica'
                                                    },
                                                    VALOR: {
                                                        title: 'Valor',
                                                        width: '30%'
                                                    }

                                                }
                                            }, function (data) { //opened handler
                                                data.childTable.jtable('load');
                                            });
                                });
                                //Return image to show on the person row
                                return $img;
                            }
                        },
                        MES: {
                            title: 'Mes',
                            width: '5%'
                        },
                        ANIO: {
                            title: 'Año',
                            width: '5%'
                        },
                        FECHA: {
                            title: 'Fecha',
                            width: '10%',
                            type: 'date',
                            displayFormat: 'yy-mm-dd'
                        },
                        SALDOANTERIOR: {
                            title: 'Saldo Anterior',
                            width: '15%'
                        },
                        TASACAMBIO: {
                            title: 'Tasa Cambio',
                            width: '10%'
                        },
                        SALDODOLARES: {
                            title: 'US Dolar',
                            width: '15%',
                            create: false
                        },
                        NIVEL: {
                            title: 'Nivel',
                            width: '30%',
                            options: '/IngresoRepFinanciero.aspx/ObtieneOpcionesNivel'
                        },
                        COMENTARIOS: {
                            title: 'Comentarios',
                            type: 'textarea',
                            list: false
                        },
                        //TABLA HIJA PARA ESTADISTICAS DEL REPORTE FINANCIERO"
                        TOTALES: {
                            title: '',
                            width: '2%',
                            sorting: false,
                            edit: false,
                            create: false,
                            display: function (reporteData) {
                                //Create an image that will be used to open child table
                                var $img = $('<img src="/Content/images/note.png" title="Mostrar Totales" />');
                                //Open child table when user clicks the image
                                $img.click(function (e) {
                                    $(function () {
                                        $("#dialog").dialog({
                                            autoOpen: false,
                                            modal: true,
                                            title: "Totales del Reporte",
                                            buttons: {
                                                Close: function () {
                                                    $(this).dialog('close');
                                                }
                                            }
                                        });
                                        //$img.click(function () {
                                            $.ajax({
                                                type: "POST",
                                                url: "/IngresoRepFinanciero.aspx/TotalesReporte",
                                                data: "{UUID_REP_FINANCIERO: '" + reporteData.record.UUID + "', TASACAMBIO: '" + reporteData.record.TASACAMBIO +"'}",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (r) {
                                                    $("#dialog").html(r.d);
                                                    $("#dialog").dialog("open");
                                                }
                                            });
                                        //});
                                    });

                                    
                                });
                                //Return image to show on the person row
                                return $img;
                            }
                        }
                    },
                    //Initialize validation logic when a form is created
                    formCreated: function (event, data) {
                        data.form.find('input[name="ANIO"]').addClass('validate[required]');
                        data.form.find('input[name="MES"]').addClass('validate[required]');
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
                //$('#IdiomaTable').jtable('reload');
                //Re-load records when user click 'load records' button.
                $('#SearchButton').click(function (e) {
                    e.preventDefault();
                    $('#IngresoRepFinanciero').jtable('load', {
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

