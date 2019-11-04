
(function () {
    'use strict';
    app.controller('listCategoriesController', controller);
    function controller($scope, $http, $state) {
        var vm = this;
        vm.add = add;
        vm.edit = edit;
        vm.remove = remove;
        vm.search = search;
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

        //SEARCH
        vm.dropdowns = [
            { field: "CategoryCode", Name: "Mã sản phẩm" },
            { field: "CategoryName", Name: "Tên danh mục" },
        ]
        function search(key, field) {
            var A = [];
          
            debugger
            if (!field) {
                toastr["warning"]("Vui lòng chọn cách thức tìm kiếm!");
            }
            else if (field === "CategoryName") {
                A.push({ field: field, operator: "contains", value: key })
                A.push({ field: "NormalizeCategoryName", operator: "contains", value: key })
                vm.grid.dataSource.filter({ logic: "or", filters: A });
            }
            else {
                A.push({ field: field, operator: "contains", value: key })
                vm.grid.dataSource.filter(A);
            }
 
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
                serverFiltering: true,
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
                }],
            //COLUMNS
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
                template: "#= kendo.toString(kendo.parseDate(CreateDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                attributes: {
                    style: "text-align: right;"
                },

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
