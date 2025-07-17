var CustomerController = function (CustomerService) {

    var init = function () {

        var indexTable = CustomerTable();

    }

   /*init end*/


    var CustomerTable = function () {

        $('#CustomerList thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#CustomerList thead');


        var dataTable = $("#CustomerList").DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            serverSide: true,
            "processing": true,
            "bProcessing": true,
            dom: 'lBfrtip',
            bRetrieve: true,
            searching: false,


            buttons: [
                {
                    extend: 'pdfHtml5',
                    customize: function (doc) {
                        doc.content.splice(0, 0, {
                            text: ""
                        });
                    },
                    exportOptions: {
                        columns: [1, 2, 3, 4]
                    }
                },
                {
                    extend: 'copyHtml5',
                    exportOptions: {
                        columns: [1, 2, 3, 4]
                    }
                },
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: [1, 2, 3, 4]
                    }
                },
                'csvHtml5'
            ],


            ajax: {
                url: '/Home/_index',
                type: 'POST',
                data: function (payLoad) {
                    return $.extend({},
                        payLoad,
                        {

                            "indexsearch": $("#Branchs").val(),
                            "branchid": $("#CurrentBranchId").val(),
                            "code": $("#md-Code").val(),
                            "ponumber": $("#md-PONumber").val(),
                            "ispost": $("#md-Post").val(),
                            "ispush": $("#md-Push").val()


                        });
                }
            },
            columns: [

                {
                    data: "id",
                    render: function (data) {

                        return "<a href=/Home/Edit/" + data + " class='edit' ><i class='editIcon' data-toggle='tooltip' title='' data-original-title='Edit'>&nbsp;</i></a>  " +
                            "<input onclick='CheckAll(this)' class='dSelected' type='checkbox' data-Id=" + data + " >"
                            ;

                    },
                    "width": "9%",
                    "orderable": false
                },
                {
                    data: "name",
                    name: "Name"

                },
                {
                    data: "code",
                    name: "Code"

                }
                ,

                {
                    data: "address",
                    name: "Address"
                }
                ,
                {
                    data: "country",
                    name: "Country"
                }

            ]

        });


        if (dataTable.columns().eq(0)) {
            dataTable.columns().eq(0).each(function (colIdx) {
               
                var cell = $('.filters th').eq($(dataTable.column(colIdx).header()).index());
                var title = $(cell).text();
             
                if ($(cell).hasClass('action')) {
                    $(cell).html(''); 

                } else if ($(cell).hasClass('bool')) {

                    $(cell).html('<select class="acc-filters filter-input " style="width:100%"  id="md-' + title.replace(/ /g, "") + '"><option>Select</option><option>Y</option><option>N</option></select>');

                } else {
                    $(cell).html('<input type="text" class="acc-filters filter-input"  placeholder="' +
                        title +
                        '"  id="md-' +
                        title.replace(/ /g, "") +
                        '"/>');
                }
            });
        }




        $("#CustomerList").on("change",
            ".acc-filters",
            function () {

                dataTable.draw();

            });
        $("#CustomerList").on("keyup",
            ".acc-filters",
            function () {

                dataTable.draw();

            });

        return dataTable;

    }


   

    return {
        init: init
    }


}(CustomerService);