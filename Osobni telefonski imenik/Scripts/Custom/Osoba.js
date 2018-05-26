$(document).ready(function () {
    $.getJSON("/Osoba/GetOsobe",
        function (json) {
            var tr;
            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + json[i].OsobaID + "</td>");
                tr.append("<td>" + json[i].Ime + "</td>");
                tr.append("<td>" + json[i].Prezime + "</td>");
                tr.append("<td>" + json[i].nazivGrada + "</td>");
                tr.append("<td>" + json[i].Opis + "</td>");
                tr.append("<td>" + json[i].Opcije + "</td>");
                $('table').append(tr);
            }

            var kanta = '<i class="fa fa-trash-o fa-2x" aria-hidden="true" id="kanta"></i>';
            var olovka = '<i class="fa fa-pencil fa-2x" aria-hidden="true" id="olovka"></i>';
            var table = $('#osobe').DataTable({
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

                            return olovka + ' | ' + kanta;
                        }
                    },
                    {
                        "targets": [0],
                        "visible": false
                    }
                ]

            });

            $('#osobe tbody').on('click', '#kanta', function () {
                var id = table.row($(this).parents('tr')).data()[0]; // dohvaća vrijednost skrivene ćelije u tablici 
                $.ajax({
                    url: '/Osoba/Delete/' + id,
                    type: "POST"
                }).done(function () {
                    alert('Uspješno Izbrisano!'),
                        setTimeout(window.location.reload.bind(window.location), 300);
                }).fail(function () {
                    alert('Nešto je pošlo po krivu, molim pokušaj ponovno!'),
                        setTimeout(window.location.reload.bind(window.location), 300);
                });
            });

            $('#osobe tbody').on('click', '#olovka', function () {
                var id = table.row($(this).parents('tr')).data()[0];
                window.location.href = "/Osoba/Edit/" + id;
            });
        });

});