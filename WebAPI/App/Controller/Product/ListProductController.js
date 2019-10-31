(function () {
    'use strict';
    app.controller('listProductController', controller);
    controller.$inject = ["$scope", "$http", "$state"];
    function controller($scope, $http, $state) {
        var vm = this;
        vm.add = add;
        vm.edit = edit;
        vm.remove = remove;
        //vm.search = search;
        //vm.products = [{}];
        //vm.currentPage = 1;
        //vm.itemsPerPage = 5;
        //vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        //vm.top = vm.itemsPerPage;
        //vm.onChangePagination = onChangePagination;
        //vm.getAllProduct = getAllProduct;
        //getAllProduct();
        //vm.sortBy = sortBy;
        //vm.sortColumn = 'Id';
        //vm.reverse = false;

        //function getAllProduct() {
        //    $http({
        //        method: "GET",
        //        url: "odata/Products?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc",
        //    }).then(function (result) {
        //        vm.products = result.data.value;
        //        vm.total = result.data["@odata.count"];
        //    })
        //}
        //function search() {
        //    $http({
        //        method: "GET",
        //        url: "api/ProductsAPI/SearchProduct?k=" + vm.k
        //    }).then(function (result) {
        //        vm.products = result.data.data;
        //        vm.total = result.data.total;
        //    })
        //}

        ////Phan trang
        //function onChangePagination() {
        //    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        //    vm.top = vm.itemsPerPage;
        //    $http({
        //        method: "GET",
        //        url: "odata/Products?$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc",
        //    }).then(function (result) {
        //        vm.products = result.data.value;
        //        vm.total = result.data["@odata.count"];
        //    })
        //}
        function add() {
            $state.go("form", {});
        }
        function edit(item) {
            $state.go("form", { id: item });
        }
        //Xoa
        function remove(item) {
            if (!confirm("Bạn có chắc muốn xóa sản phẩm này!")) {
                return false;
            }
            $http({
                method: 'delete',
                //url: "api/ProductsAPI/Delete?key=" + item.Id
                url: "/odata/Products" + "(" + item + ")",
            }).then(function (response) {
                vm.grid.dataSource.read();
                toastr["success"]("Xóa thành công!")
            }, function (error) {
                toastr["error"]("Không thể xóa sản phẩm đã có trong đơn hàng!")
            });
        }
        ////Sap xep
        //function sortBy(col, reverse, show) {
        //    switch (col) {

        //        case "Name": {
        //            vm.sortColumn = 'Name'; break;
        //        }
        //        case "Category": {
        //            vm.sortColumn = 'Category'; break;
        //        }
        //        case "Price": {
        //            vm.sortColumn = 'Price'; break;
        //        }
        //    }
        //    vm.reverse = !reverse;
        //}

        //KENDO GRID CONFIG
        vm.mainGridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: "/odata/Products",
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
                },"search"
            ],
            columns: [{
                field: "ProductCode",
                title: "Mã sản phẩm",
                width: "200px"
            }, {
                field: "Name",
                title: "Tên sản phẩm",
                width: "200px"
            }, {
                field: "ProductCategoryName",
                title: "Danh mục sản phẩm",
                width: "200px"
            }, {
                field: "Price",
                title: "Giá",
                width: "200px",
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
                //Thuoc tinh format
                attributes: {
                    style: "text-align: right;"
                },
                format: "{0:0,} VNĐ",

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
    }
})();
