
(function () {
    'use strict';
    app.controller('listCategoriesController', controller);
    function controller($scope, $http, $state) {
        var vm = this;
        vm.add = add;
        vm.edit = edit;
        vm.remove = remove;

        //vm.categories = {};
        //vm.getCategories = getCategories;
        //vm.currentPage = 1;
        //vm.itemsPerPage =5;
        //vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        //vm.top = vm.itemsPerPage;
        //vm.onChangePagination = onChangePagination;

        ////GET Categories
        //getCategories();
        //function getCategories() {
        //    debugger
        //    $http({
        //        method: "GET",
        //        //url: "api/ProductCategoriesAPI/ProductCategories?skip=" + vm.skip + "&take=" + vm.take
        //        url: "odata/ProductCategories?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top+"&$orderby=Id desc"
        //    }).then(function successCallback(res) {
        //        vm.categories = res.data.value;
        //        vm.total = res.data["@odata.count"];
        //    }, function errorCallback(res) {
        //        toastr["error"]("Có lỗi rồi, chưa thể tải được dữ liệu");
        //    });
        //}
        ////Phan trang
        //function onChangePagination() {
        //    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        //    vm.top = vm.itemsPerPage;
        //    $http({
        //        method: "GET",
        //        url: "odata/ProductCategories?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top + "&$orderby=Id desc"
        //    }).then(function successCallback(res) {
        //        vm.categories = res.data.value;
        //        vm.total = res.data["@odata.count"];
        //    })
        //}



        //Add 

        function add() {
            $state.go("categoriesForm", {})
        }
        function edit(item) {
            debugger;
            $state.go("categoriesForm", { id: item })
        }
        function remove(item) {
            if (!confirm("Có chắc muốn xóa không??")) {
                return false;
            }
            $http({
                method: "DELETE",
                //url:"api/ProductCategoriesAPI/ProductCategories?Id=" +item.Id
                url: "/odata/ProductCategories" + "(" + item + ")",
            }).then(function successCallback(res) {
                
                vm.grid.dataSource.read();
                toastr["success"]("Đã xóa thành công");
            }, function errorCallback() {
                toastr["error"]("Không thể xóa danh mục đã có sản phẩm!");
            })
        }


        //KENDO GRID CONFIG
        vm.mainGridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: "/odata/ProductCategories",
                },
                serverPaging: true,
                serverSorting: true,
                sort: { field: "Id", dir: "desc" },
            },
            sortable: true,
            pageable: {
                pageSize: 5,
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
                field: "CategoryCode",
                title: "Mã danh mục",
                width: "200px"
            }, {
                field: "CategoryName",
                title: "Tên danh mục ",
                width: "200px"
            }, {
                field: "CreateDate",
                title: "Ngày cập nhật",
                width: "200px",
                template: "#= kendo.toString(kendo.parseDate(CreateDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
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
        //SEARCH
        vm.dropdowns = [
            { Id: 1, Name: "Mã danh mục" },
            { Id: 2, Name: "Tên danh mục" },
            { Id: 3, Name: "Ngày cập nhật" },
        ]
        vm.search = search;
        function search(key, id) {
            debugger
            switch (id) {
                case "1": {
                    $http({
                        method: "GET",
                        url: "odata/ProductCategories?" + "$filter=contains(CategoryCode," + "'" + key + "'" + ")"
                    }).then(function (res) {
                        vm.grid.dataSource = res.data;

                        debugger
                    })
                    break;
                }
                case "2": {
                    $http({
                        method: "GET",
                        url: "odata/ProductCategories?" + "$filter=contains(CategoryName," + "'" + key + "'" + ")"
                    }).then(function (res) {
                    })
                    break;
                }
                case "3": {
                    $http({
                        method: "GET",
                        url: "odata/ProductCategories?" + "$filter=contains(CreateDate," + "'" + key + "'" + ")"
                    }).then(function (res) {
                    })
                    break;
                }
                default: {
                    toastr["warning"]("Bạn chưa chọn cách thức tìm kiếm, vui lòng thử lại !")
                }
            }
        }
    }
})();
