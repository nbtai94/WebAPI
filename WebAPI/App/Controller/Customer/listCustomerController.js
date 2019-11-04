(function () {
    'use strict';
    app.controller('listCustomerController', controller);
    controller.$inject = ["$http", "$state", "$scope"];
    function controller($http, $state, $scope) {
        var vm = this;
        vm.add = add;
        vm.edit = edit;
        vm.remove = remove;
        vm.search = search;
        //vm.customers = [{}];
        //vm.currentPage = 1;
        //vm.itemsPerPage =5;
        //vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        //vm.top = vm.itemsPerPage;
        //vm.onChangePagination = onChangePagination;
        //vm.getAllCustomer = getAllCustomer;
        //vm.sortBy = sortBy;
        //vm.reverse = false;
        //vm.search = search;
        //getAllCustomer();
        ////Get All Product

        //function getAllCustomer() {
        //    $http({
        //        method: "GET",
        //        //url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        //        url: "odata/Customers?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc",
        //    }).then(function (result) {
        //        vm.customers = result.data.value;
        //        vm.total = result.data["@odata.count"];
        //    })
        //}
        //Serach
        //function search() {
        //    $http({
        //        method: "GET",
        //        url: "api/Customers/SearchCustomer?key=" + vm.k
        //    }).then(function (result) {
        //        vm.customers = result.data.data;
        //        vm.total = result.data.total;
        //    })
        //}
        ////Phan trang
        //function onChangePagination() {
        //    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        //    vm.top = vm.itemsPerPage;
        //    $http({
        //        method: "GET",
        //        url: "odata/Customers?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc",

        //    }).then(function (result) {
        //        vm.customers = result.data.value;
        //        vm.total = result.data["@odata.count"];
        //    })
        //}
        ////Sap xep
        //function sortBy(col, reverse) {
        //    switch (col) {
        //        case "Name": {
        //            vm.sortColumn = 'Name'; break;
        //        }
        //        case "Address": {
        //            vm.sortColumn = 'Address'; break;
        //        }
        //        case "Email": {
        //            vm.sortColumn = 'Email'; break;
        //        }
        //        case "Phone": {
        //            vm.sortColumn = 'Email'; break;
        //        }
        //    }
        //    vm.reverse = !reverse;
        //}
        //Redirect sang form
        function add() {
            $state.go("cusForm", {});
        }
        function edit(item) {
            $state.go("cusForm", { id: item });
        }
        //Xoa
        function remove(item) {
            if (!confirm("Bạn có chắc muốn xóa khách hàng này!")) {
                return false;
            }
            $http({
                method: 'delete',
                url: "/odata/Customers" + "(" + item + ")",
            }).then(function successCallback() {
                vm.grid.dataSource.read();
                toastr["success"]("Xóa thành công!");
            }, function errorCallback() {
                toastr["error"]("Không thể xóa khách đã đặt hàng!")
            });
        }
        //SEARCH
        vm.dropdowns = [
            { field: "Name", Name: "Tên" },
            { field: "Address", Name: "Địa chỉ" },
            { field: "Phone", Name: "Số điện thoại" },
            { field: "Email", Name: "Email" },
            { field: "CustomerCode", Name: "Mã khách hàng" }
        ]
        function search(key = "", field) {
            var A = [];
            debugger
            if (!field) {
                toastr["warning"]("Vui lòng chọn cách thức tìm kiếm!");
            }
            else if (field === "Name") {
                A.push({ field: field, operator: "contains", value: key })
                A.push({ field: "NormalizeName", operator: "contains", value: key })
                vm.grid.dataSource.filter({ logic: "or", filters: A });
            }
            else {
                A.push({ field: field, operator: "contains", value: key })
                vm.grid.dataSource.filter(A);
            }
        }
        //KENDO GRID config
        vm.mainGridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: "/odata/Customers",
                },
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                sort: { field: "Id", dir: "desc" },//sắp xếp id giảm dần
            },
            
            sortable: true,
            pageable: {
                pageSize: 10,
                refresh: true
            },
            toolbar: [{
                template: '<a class="k-button" ng-click="vm.add()">Thêm</a>'
            },
            ],
            columns: [
                {
                    field: "CustomerCode",
                    title: "Mã khách hàng",
                    width: "150px",

                }, {
                    field: "Name",
                    title: "Tên khách hàng",
                    width: "200px",

                }, {
                    field: "Address",
                    title: "Địa chỉ",
                    width: "300px",

                }, {
                    field: "Phone",
                    title: "Số điện thoại",
                    width: "200px"
                }, {
                    field: "Email",
                    title: "Email",
                    width: "200px"
                },
                {
                    command: [{
                        template: "<a class='k-button k-grid-settings' ng-click='vm.edit(dataItem.Id)'><span class='k-icon k-i-edit'></span> Sửa</a>",
                    }, {
                        template: "<a class='k-button  k-grid-settings' ng-click='vm.remove(dataItem.Id)'><span class='k-icon k-i-delete'></span> Xóa</a>",

                    }], title: "&nbsp;", width: "150px"
                }],
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
        };
    }
})();
