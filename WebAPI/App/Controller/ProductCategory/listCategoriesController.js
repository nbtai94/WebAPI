
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
        vm.take = vm.itemsPerPage;
        vm.onChangePagination = onChangePagination;
  
        //Phan trang
        function onChangePagination() {
            vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
            vm.take = vm.itemsPerPage;
            $http({
                method: "GET",
                url: "api/ProductCategoriesAPI/ProductCategories?skip=" + vm.skip + "&take=" + vm.take
            }).then(function successCallback(res) {
                vm.categories = res.data.data;
                vm.total = res.data.total;
            })
        }
        //GET Categories
        function getCategories() {
            debugger
            $http({
                method: "GET",
                url: "api/ProductCategoriesAPI/ProductCategories?skip=" + vm.skip + "&take=" + vm.take
            }).then(function successCallback(res) {
                vm.categories = res.data.data;
                vm.total = res.data.total;
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
            debugger;
            $http({
                method: "DELETE",
                url:"api/ProductCategoriesAPI/ProductCategories?Id=" +item.Id
            }).then(function successCallback(res) {
                toastr["success"]("Đã xóa thành công");
                getCategories();
            }, function errorCallback() {
                    toastr["error"]("Lỗi rồi bạn ơi thử lại đi!");
            })
        }
    }
})();
