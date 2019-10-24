
(function () {
    'use strict';
    app.controller('listCategoriesController', controller);
    function controller($scope,$http,$state) {
        var vm = this;
        vm.categories = {};
        vm.getCategories = getCategories;
        vm.currentPage = 1;
        vm.itemsPerPage =5;
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.top = vm.itemsPerPage;
        vm.onChangePagination = onChangePagination;
  
        //Phan trang
        function onChangePagination() {
            vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
            vm.top = vm.itemsPerPage;
            $http({
                method: "GET",
                url: "odata/ProductCategories?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top
            }).then(function successCallback(res) {
                vm.categories = res.data.value;
                vm.total = res.data["@odata.count"];
            })
        }
        //GET Categories
        function getCategories() {
            debugger
            $http({
                method: "GET",
                //url: "api/ProductCategoriesAPI/ProductCategories?skip=" + vm.skip + "&take=" + vm.take
                url:"odata/ProductCategories?"+"$count=true"+"&$skip="+vm.skip+"&$top="+vm.top
            }).then(function successCallback(res) {
                vm.categories = res.data.value;
                vm.total = res.data["@odata.count"];
            }, function errorCallback(res) {
                toastr["error"]("Có lỗi rồi, chưa thể tải được dữ liệu");
            });                    
        }
        getCategories();

        //Add 
        vm.add = add;
        function add() {
            $state.go("categoriesForm", {})        
        }
        vm.edit = edit;
        function edit(item) {
            debugger;
            $state.go("categoriesForm", {id:item.Id})
        }
        vm.remove = remove;
        function remove(item) {
            if (!confirm("Có chắc muốn xóa không??")) {
                return false;
            }
            debugger;
            $http({
                method: "DELETE",
                //url:"api/ProductCategoriesAPI/ProductCategories?Id=" +item.Id
                url:"odata/ProductCategories"+"(" +item.Id +")",
            }).then(function successCallback(res) {
                toastr["success"]("Đã xóa thành công");
                getCategories();
            }, function errorCallback() {
                    toastr["error"]("Không thể xóa danh mục đã có sản phẩm!");
            })
        }

        


    }
})();
