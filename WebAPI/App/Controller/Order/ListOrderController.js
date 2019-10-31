app.controller("ListOrderController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
    vm.orders = [{}];
    vm.add = add;
    vm.remove = remove;
    vm.info = info;
    //vm.getAllOrder = getAllOrder;
    //vm.currentPage = 1;
    //vm.itemsPerPage = 5;
    //vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    //vm.top = vm.itemsPerPage;
    //vm.getAllOrder();
    ////Get All
    //function getAllOrder() {
    //    $http({
    //        method: "GET",
    //        url: "odata/Orders?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc",
    //    }).then(function (result) {
    //        vm.orders = result.data.value;
    //        vm.total = result.data["@odata.count"];
    //    })
    //}
    //Tim kiem
    //vm.search = search;
    //function search() {
    //    $http({
    //        method: "GET",
    //        url: "api/OrdersAPI/Orders?key=" + vm.k
    //    }).then(function (result) {
    //        vm.orders = result.data.data;
    //        vm.total = result.data.total;
    //    })
    //}
    ////Phan trang
    //vm.onChangePagination = onChangePagination;
    //function onChangePagination() {
    //    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    //    vm.take = vm.itemsPerPage;
    //    $http({
    //        method: "GET",
    //        url: "odata/Orders?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc",
    //    }).then(function (result) {
    //        vm.orders = result.data.value;
    //        vm.total = result.data["@odata.count"];
    //    })
    //}
    ////Sap xep
    //vm.sortBy = sortBy;
    //vm.reverse = false;
    //function sortBy(col, reverse) {
    //    switch (col) {
    //        case "CustomerName": {
    //            vm.sortColumn = 'CustomerName'; break;
    //        }
    //        case "CustomerAddress": {
    //            vm.sortColumn = 'CustomerAddress'; break;
    //        }
    //        case "CustomerPhone": {
    //            vm.sortColumn = 'CustomerPhone'; break;
    //        }
    //        case "TotalMoney": {
    //            vm.sortColumn = 'TotalMoney'; break;
    //        }
    //        case "DateOrder": {
    //            vm.sortColumn = 'DateOrder'; break;
    //        }
    //    }
    //    vm.reverse = !reverse;
    //}
    //Redirect sang form
    function add() {
        $state.go("orderform", {});
    }
    vm.edit = edit;
    function edit(item) {
        $state.go("orderform", { id: item });
    }
    function info(item) {
        $state.go("orderdetail", { id: item });
    }
    //Remove & Redirect
    function remove(item) {
        if (!confirm("Bạn có chắc muốn xóa đơn hàng này!")) {
            return false;
        }
        $http({
            method: "DELETE",
            url: "/odata/Orders" + "(" + item + ")",

        }).then(function (res) {
            vm.grid.dataSource.read();
            toastr["success"]("Đã xóa đơn hàng!")
        });
    }

    //KENDO GRID config
    vm.mainGridOptions = {
        dataSource: {
            type: "odata-v4",
            transport: {
                read: "/odata/Orders",
            },
            serverPaging: true,
            serverSorting: true,
            sort: { field: "Id", dir: "desc" },
        },
        sortable: true,
        pageable: {
            pageSize: 10,
            refresh: true
        },
  
    

        dataBound: function () {
            this.expandRow(this.tbody.find("tr.k-master-row").first());
        },
        toolbar: [
            {
                template: '<a class="k-button" ng-click="vm.add()">Thêm</a>'
            }
        ],
        columns: [{
            field: "OrderCode",
            title: "Mã đơn hàng",
            width: "100px"
        }, {
            field: "CustomerName",
            title: "Tên khách hàng",
            width: "150px"
        }, {
            field: "CustomerAddress",
            title: "Địa chỉ khách hàng",
            width: "200px"
        }, {
            field: "CustomerPhone",
            title: "Số điện thoại",
            width: "150px"
        }, {
            field: "TotalMoney",
            title: "Tổng tiền",
            width: "100px",
            format: "{0:0,} VNĐ",
            attributes: {
                style: "text-align: right;"
            },
            //Bo loc
            filterable: {
                extra: false,
                messages: {
                    info: 'Phương thức lọc:',
                    filter: "Lọc",
                    clear: "Hủy lọc"
                },
                operators: {
                    string: {
                        eq: "Bằng",
                        gte: "Lớn hơn hoặc bằng",
                        gt: "Lớn hơn",
                        lte: "Nhỏ hơn hoặc bằng",
                        lt: "Nhỏ hơn"

                    }
                }
            },
        },
        {
            field: "DateOrder",
            title: "Ngày đặt hàng",
            width: "200px",
            template: "#= kendo.toString(kendo.parseDate(DateOrder, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            ,
            attributes: {
                style: "text-align: right;"
            }
        },
        {
            command: [
                {
                    template: "<a class='k-button k-grid-settings' ng-click='vm.edit(dataItem.Id)'><span class='k-icon k-i-edit'></span> Sửa</a>",
                },
                {
                    template: "<a class='k-button  k-grid-settings' ng-click='vm.remove(dataItem.Id)'><span class='k-icon k-i-delete'></span> Xóa</a>",

                },
            ], title: "&nbsp;", width: "100px"
        },
        ]
    };

});