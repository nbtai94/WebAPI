app.controller('ListCustomer', function ($scope, $http, $state) {

    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.search = search;
    vm.customers = {};
    vm.currentPage = 1;
    vm.itemsPerPage = 10;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;
    vm.onChangePagination = onChangePagination;
    vm.getAllCustomer = getAllCustomer;
    getAllCustomer();

    vm.changeItemPerPage = changeItemPerPage;

    function getAllCustomer() {
        debugger;
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }


    function search() {
        debugger;
        $http({
            method: "GET",
            url: "api/Customers/SearchCustomer?key=" + vm.k
        }).then(function (result) {
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }



    function changeItemPerPage() {
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.take = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
       
    }



    function onChangePagination() {
        debugger;
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.take = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
            vm.total = result.data.total;

        })
    }

    function add() {
        $state.go("cusForm", {});
    }
    function edit(item) {
        $state.go("cusForm", { id: item.Id });
    }
    function remove(item) {
        if (!confirm("Bạn có chắc muốn xóa!")) {
            return false;
        }

        $http({
            method: 'delete',
            url: "api/Customers/RemoveCustomer?Id=" + item.Id
        }).then(function (response) {
            debugger;
            alert("Đã xóa thành công!");
            getAllCustomer();
        }, function (error) {
        });
    }
});

