$(document).ready(function () {
    $('#NivelTable').jtable({
        title: 'Lista Niveles',
        paging: true,
        pageSize: 10,
        sorting: true,
        actions: {
            listAction: '/Nivel/ObtieneListaNivel',
            createAction: '/Nivel/Create',
            updateAction: '/Nivel/Edit',
            deleteAction: '/Nivel/Delete'
        },
        fields: {
            Uuid: {
                key: true,
                list: false
            },
            Nivel: {
                title: 'Nivel',
                width: '15%'
            },
            Identificador: {
                title: 'Identificador',
                width: '45%'
            },
            Superior: {
                title: 'Superior',
                width: '45%',
            }
        }
    });
    $('#NivelTable').jtable('reload');
});