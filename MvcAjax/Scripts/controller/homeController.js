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
        $("#frmSaveData").validate({
            rules: {
                txtName: {
                    required: true,
                    minlength: 3,
                },
                txtSalaryEdit: {
                    required: true,
                    minlength: 6
                }
            },
            messages: {
                txtName: "Nhập tên",
                txtSalaryEdit: "Nhập lương"
            }
        });

        $('#txtSalary').off('keypress').on('keypress', function (e) {
            if (e.which === 13) {
                var id = $(this).data('id');
                var value = $(this).val();
                homeController.updateSalary(id, value);
            }
        });

        $('#txtNameSearch').off('keypress').on('keypress', function (e) {
            if (e.which === 13) {
                homeController.loadData(true);
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
            if ($("#frmSaveData").valid()) {
                homeController.saveData();
            }
        });

        $('.btn-edit').off('click').on('click', function () {
            $("#modalAddUpdate").modal('show');
            var id = $(this).data('id');
            homeController.loadDetail(id);
        });

        $('.btn-delete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm("Are you sure delete this?", function (result) {
                homeController.deleteEmployee(id);
            });
        });

        $('#btnSearch').off('click').on('click', function () {
            homeController.loadData(true);
        });

        $('#btnReset').off('click').on('click', function () {
            $("#txtNameSearch").val('');
            $("#ddlActive").val('');
            homeController.loadData(true);
        });
    },
    deleteEmployee: function (id) {
        $.ajax({
            url: '/Home/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status === true) {
                    bootbox.alert("Delete Success", function () {
                        $("#modalAddUpdate").modal('hide');
                        homeController.loadData(true);
                    });
                }
                else {
                    bootbox.alert(response.Message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status === true) {
                    var data = response.data;
                    $('#hidD').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtSalaryEdit').val(data.Salary);
                    $('#ckStatus').prop('checked', data.Status);
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
    saveData: function () {
        var name = $('#txtName').val();
        var salary = parseFloat($('#txtSalaryEdit').val());
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
                if (response.status === true) {
                    bootbox.alert("Save Success", function () {
                        $("#modalAddUpdate").modal('hide');
                        homeController.loadData(true);
                    })

                }
                else {
                    bootbox.alert(response.Message);
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
    loadData: function (changePageSize) {
        var name = $('#txtNameSearch').val();
        var status = $('#ddlActive').val();
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                name: name,
                status: status,
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
                    }, changePageSize)
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
            data: { model: JSON.stringify(data) },
            success: function (response) {
                if (response.status) {
                    bootbox.alert('Update success');
                }
                else {
                    bootbox.alert(response.Message);
                }
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
    paging: function (totalRow, callback, changePageSize) {

        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData('twbs-pagination');
            $('#pagination').unbind('page');
        }

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