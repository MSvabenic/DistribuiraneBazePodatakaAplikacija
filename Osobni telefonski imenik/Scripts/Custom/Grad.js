$(document).ready(function () {
    $.getJSON("/Grad/GetGradovi",
        function (json) {
            var tr;
            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + json[i].GradID + "</td>");
                tr.append("<td>" + json[i].Naziv + "</td>");
                tr.append("<td>" + json[i].nazivDrzave + "</td>");
                tr.append("<td>" + json[i].Opcije + "</td>");
                $('table').append(tr);
            }

            var kanta = '<i class="fa fa-trash-o fa-2x" aria-hidden="true" id="kanta"></i>';
            var olovka = '<i class="fa fa-pencil fa-2x" aria-hidden="true" id="olovka"></i>';
            var table = $('#gradovi').DataTable({
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

            $('#gradovi tbody').on('click', '#kanta', function () {
                var id = table.row($(this).parents('tr')).data()[0]; // dohvaća vrijednost skrivene ćelije u tablici 
                $.ajax({
                    url: '/Grad/Delete/' + id,
                    type: "POST"
                }).done(function () {
                    alert('Uspješno Izbrisano!'),
                        setTimeout(window.location.reload.bind(window.location), 300);
                }).fail(function () {
                    alert('Nešto je pošlo po krivu, molim pokušaj ponovno!'),
                        setTimeout(window.location.reload.bind(window.location), 300);
                });
            });

            $('#gradovi tbody').on('click', '#olovka', function () {
                var id = table.row($(this).parents('tr')).data()[0];
                window.location.href = "/Grad/Edit/" + id;
            });
        });

});