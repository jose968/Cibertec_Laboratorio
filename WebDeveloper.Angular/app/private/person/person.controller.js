(function () {
    'user strict';

    angular.module('app')
        .controller('personController', personController);


    personController.$inject = ['dataService'];

    function personController(dataService) {
        var vm = this;
        vm.title = 'Person Controller';
        var apiUrl = 'http://localhost/WebDeveloper.API/Person/';
        //variables
        vm.personList = [];
        vm.person;
        vm.readOnly = false;
        vm.isDelete = false;
        vm.buttonTitle = "";
        
        //function
        vm.create = create;
        vm.update = update;
        vm.details = details;
        vm.delete = modalDelete;
        vm.getPersonDetail = getPersonDetail;
        vm.personUpdate = personUpdate;

        vm.modalFunction;


        init();

        function init() {
            loadData();
        }

        function loadData() {
            vm.personList = [];
            var url = apiUrl + 'list/1/15';
            dataService.getData(url)
                .then(function (result) {
                    vm.personList = result.data;
                },
                function (error) {
                    console.log(error);
                });
        }

        function getPersonDetail(id) {
            var url = apiUrl + id;
            dataService.getData(url).then(
                function (result) {
                    vm.person = result.data;
                }
                );

        }

        function create() {
            vm.person = {};
            vm.modalFunction = personCreate;
            vm.readOnly = false;
            vm.isDelete = false;
            vm.buttonTitle = "Create"
        }

        function update() {
            vm.modalFunction = personUpdate;
            vm.buttonTitle = "Update";
            vm.readOnly = false;
            vm.isDelete = false;
        }

        function modalDelete() {
            vm.readOnly = true;
            vm.isDelete = true;
            vm.modalFunction = personDelete;
        }

        function personCreate() {
            dataService.putData(apiUrl, vm.person)
                .then(function () {
                    loadData();
                    closeModal();
                })
        }

        function personUpdate() {
            dataService.postData(apiUrl, vm.person)
                .then(function () {
                    loadData();
                    closeModal();
                })
        }

        function personDelete() {
            dataService.deleteData(apiUrl + vm.person.businessEntityID)
                .then(function () {
                    loadData();
                    closeModal();
                })
        }
       
        function details() {
            vm.readOnly = true;
            vm.isDelete = false;
        }
        function closeModal() {
            $('button[data-dismiss="modal"]').click();
        }


    }

})();