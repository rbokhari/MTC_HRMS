'use strict';

invModule.directive("ngPrint", function () {

    console.log('ngPrint');

    var printSection = document.getElementById('printSection');

    if (!printSection) {
        printSection = document.createElement('div');
        printSection.id = 'printSection';
        document.body.appendChild(printSection);
        console.log('!printSection');
    }

    function printElement(elem) {
        // clones the element you want to print
        console.log(elem);
        //var domClone = elem.cloneNode(true);
        if (!printSection) {
            printSection = document.createElement('div');
            printSection.id = 'printSection';
            document.body.appendChild(printSection);
            console.log('!printSection inside');
        }
        printSection.appendChild(elem);
    }


    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.on('click', function () {
                console.log('print called');
                var elemToPrint = document.getElementById(attrs.printElementId);
                if (elemToPrint) {
                    printElement(elemToPrint);
                    window.print(elemToPrint);
                }
            });

            window.onafterprint = function () {
                // clean the print section before adding new content
                printSection.innerHTML = '';
            }
        }

    };
});