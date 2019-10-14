﻿var app = angular.module("app", ['ui.router', 'ui.bootstrap',"kendo.directives"]);
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');
    $stateProvider
        .state('home', {
            url: '/Home',  //Tên hiển thị trên URL
            template: '<div ui-view></div>',
            templateUrl: 'home/index'  //Đường dẫn file
        })
        .state('list', {
            url: '/ListProduct',
            //template: '<div ui-view></div>',
            templateUrl: 'Product/ListProduct'
        })
        .state('contact', {
            url: '/Contact',
            //template: '<div ui-view></div>',
            templateUrl: 'home/contact'
        })
        .state('customer', {
            url: '/ListCustomer',
            //template: '<div ui-view></div>',
            templateUrl: 'Customer/ListCustomer'
        })

        .state('form', { 
            url: '/Form?id',
            //template: '<div ui-view></div>',
            templateUrl: 'Product/ProductForm'
        })
        .state('cusForm', {
            url: '/CusForm?id',
            //template: '<div ui-view></div>',
            templateUrl: 'Customer/CustomerForm'
        })
        .state('order', {
            url: '/ListOrder',
            //template: '<div ui-view></div>',
            templateUrl: 'Orderss/ListOrder'
        })
     
        .state('orderform', {
            url: '/OrderInfo?id',
            //template: '<div ui-view></div>',
            templateUrl: 'Orderss/OrderForm'
        })
        .state('orderdetail', {
            url: '/OrderInfo?id',
            //template: '<div ui-view></div>',
            templateUrl: 'Orderss/OrderDetail'
        })
});

app.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);