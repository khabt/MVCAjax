var homeConfig = {
    pageSize: 12,
    pageIndex: 1,
}

var homeController = {
    init: function () {
        homeController.registerEvent();
        homeController.loadData();
        homeController.loadDataFromDB();
    },
    registerEvent: function () {
        $('#txtSalary').off('keypress').on('keypress', function (e) {
            if (e.which === 13) {
                var id = $(this).data('id');
                var value = $(this).val();
                homeController.updateSalary(id, value);
            }
        });

        //$('#txtFirstName').off('keypress').on('keypress', function (e) {
        //    if (e.which === 13) {
        //        var id = $(this).data('id');
        //        var value = $(this).val();
        //        homeController.updateName(id, value, "KAAAAAA");
        //    }
        //})
        $("#btnAddNew").off('click').on('click', function () {
            $("#modalAddUpdate").modal('show');
            homeController.resetForm();
        });

        $('#btnSave').off('click').on('click', function () {
            homeController.saveData();
        })
    },
    saveData: function () {
        var name = $('#txtName').val();
        var salary = parseFloat($('#txtSalary').val());
        var status = $('#ckStatus').prop('checked');
        var id = parseInt($('#hidD').val());

        var employee = {
            Name: name,
            Salary: salary,
            Status: status,
            ID: id
        }
        $.ajax({
            url: '/Home/SaveData',
            data: {
                StrEmployee: JSON.stringify(employee)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (status == true) {
                    alert('Success');
                    $("#modalAddUpdate").modal('hide');
                    homeController.loadData();
                }
                else {
                    alert(response.Message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtName').val('');
        $('txtSalary').val(0);
        $('#ckStatus').prop('checked', true);
    },
    loadData: function () {
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    //alert(response.status);
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Salary: item.Salary,
                            Status: item.Status === true ? "<span class=\"label label-success\">Active</span>" : "<span class=\"label label-danger\">Locked</span>"
                        });
                    });
                    $('#tblData').html(html);
                    homeController.paging(response.total, function () {
                        homeController.loadData();
                    })
                    homeController.registerEvent();
                }
            }
        })
    },
    updateSalary: function (id, value) {
        var data = {
            ID: id,
            Salary: value
        };
        $.ajax({
            url: '/Home/Update',
            type: 'POST',
            dataType: 'json',
            data: { model: JSON.stringify(data)},
            success: function (response) {
                if (response.status) {
                    alert('Update successed');
                }
                else { alert('Update failed');}
            }
        })
    },
    loadDataFromDB: function () {
        $.ajax({
            url: '/Home/LoadDataFromData',
            type: 'GET',
            data: {
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    //alert(response.status);
                    var data = response.data;
                    var html = '';
                    var template = $('#data-templatedb').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            EmployeeID: item.EmployeeID,
                            FullName: item.Sex + ' ' + item.FirstName + ' ' + item.LastName,
                            LastName: item.LastName,
                            FirstName: item.FirstName,
                            Title: item.Title,
                            Sex: item.Sex,
                            BirthDate: item.BirthDate,
                            HireDate: item.HireDate,
                        });
                    });
                    $('#tblDataDB').html(html);
                    homeController.pagingDB(response.total, function () {
                        homeController.loadDataFromDB();
                    })

                }
            }
        })
    },
    updateName: function (EmployeeID, FirstName, LastName) {
        var data = {
            EmployeeID: EmployeeID,
            LastName: LastName,
            FirstName: FirstName
        };
        $.ajax({
            url: '/Home/UpdateName',
            type: 'POST',
            dataType: 'json',
            data: { model: JSON.stringify(data) },
            success: function (response) {
                if (response.status) {
                    alert('Update successed');
                }
                else { alert('Update failed'); }
            }
        })
    },
    paging: function (totalRow, callback) {
        var totalPage = Math.ceil(totalRow / homeConfig.pageSize)
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            visiblePages: 10,
            first: "Đầu",
            prev: "Trước",
            next: "Tiếp",
            last: "Cuối",
            onPageClick: function (event, page) {
                homeConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    pagingDB: function (totalRow, callback) {
        var totalPage = Math.ceil(totalRow / homeConfig.pageSize)
        $('#paginationDB').twbsPagination({
            totalPages: totalPage,
            visiblePages: 10,
            first: "Đầu",
            prev: "Trước",
            next: "Tiếp",
            last: "Cuối",
            onPageClick: function (event, page) {
                homeConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
homeController.init();