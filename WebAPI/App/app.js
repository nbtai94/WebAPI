var app = angular.module("app", ['ui.router', 'ui.bootstrap', "kendo.directives"]);
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/dashboard'); // Trang mac dinh
    $stateProvider
        .state('home', {
            url: '/Home',  //Tên hiển thị trên URL
            template: '<div ui-view></div>',    
            templateUrl: '/home/index',

        })
        .state('dashboard', {
            url: '/dashboard',
            //template: '<div ui-view></div>',
            templateUrl: '/home/dashboard'
        })
        .state('categories', {
            url: '/Categories',
            //template: '<div ui-view></div>',
            templateUrl: '/ProductCategory/ListCategories'
        })
        .state('categoriesForm', {
            url: '/categoriesForm?id',
            //template: '<div ui-view></div>',
            templateUrl: '/ProductCategory/CategoryForm'
        })



        .state('list', {
            url: '/ListProduct',
            //template: '<div ui-view></div>',
            templateUrl: '/Product/ListProduct'
        })
        .state('contact', {
            url: '/Contact',
            //template: '<div ui-view></div>',
            templateUrl: '/home/contact'
        })
        .state('customer', {
            url: '/ListCustomer',
            //template: '<div ui-view></div>',
            templateUrl: '/Customer/ListCustomer'
        })

        .state('form', { 
            url: '/Form?id',
            //template: '<div ui-view></div>',
            templateUrl: '/Product/ProductForm'
        })
        .state('cusForm', {
            url: '/CusForm?id',
            //template: '<div ui-view></div>',
            templateUrl: '/Customer/CustomerForm'
        })
        .state('order', {
            url: '/ListOrder',
            //template: '<div ui-view></div>',
            templateUrl: '/Orders/ListOrder'
        })
     
        .state('orderform', {
            url: '/OrderInfo?id',
            //template: '<div ui-view></div>',
            templateUrl: '/Orders/OrderForm'
        })
        .state('orderdetail', {
            url: '/OrderInfo?id',
            //template: '<div ui-view></div>',
            templateUrl: '/Orders/OrderDetail'
        })
});

app.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);