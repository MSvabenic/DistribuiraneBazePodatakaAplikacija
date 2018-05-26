$(document).ready(function () {
    $.getJSON("/Brojevi/GetSviBrojevi/",
        function (json) {
            var tr;
            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + json[i].OsobaId + "</td>");
                tr.append("<td>" + json[i].Ime + "</td>");
                tr.append("<td>" + json[i].Prezime + "</td>");
                tr.append("<td>" + json[i].Brojevi + "</td>");
                tr.append("<td>" + json[i].Opcije + "</td>");
                $('table').append(tr);
            }

            var kanta = '<i class="fa fa-trash-o fa-2x" aria-hidden="true" id="kanta"></i>';
            var olovka = '<i class="fa fa-pencil fa-2x" aria-hidden="true" id="olovka"></i>';
            var table = $('#brojeviosobe').DataTable({
                "order": [[1, "asc"]],
                "autoWidth": true,
                "oLanguage": {
                    "sUrl": "https://cdn.datatables.net/plug-ins/1.10.16/i18n/Croatian.json"
                },
                "columnDefs": [
                    {
                        "className": "dt-center", "targets": "_all"
                    },
                    {
                        "targets": -1,
                        "data": null,
                        "render": function () {

                            return olovka;
                        }
                    },
                    {
                        "targets": [0],
                        "visible": false
                    }
                ]

            });

            $('#brojeviosobe tbody').on('click', '#olovka', function () {
                var id = table.row($(this).parents('tr')).data()[0];
                window.location.href = "/Brojevi/EditIndex/" + id;
            });
        });

});