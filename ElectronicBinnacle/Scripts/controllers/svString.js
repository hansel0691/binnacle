angular.module('bitacora.controllers.SvString', [])
    .controller('SvStringCtrl', [
            '$scope', 'samplesFact',
            function ($scope, samplesFact) {
                var allPacks = $scope.order.WorkPackages.map(function (wp) { return wp.Packages; }).reduce(concatArray).sort(function (a) { return a.Standard; });
                var allParamsIdentifier = [];
                var nonStandartParamsIds = [];
                var standartPacksId = [];
                for (var i = 0; i < allPacks.length; i++) {
                    var pack = allPacks[i];
                    if (pack.Standard) {
                        if (standartPacksId.indexOf(pack.PackageId) == -1) {
                            allParamsIdentifier.push({ identifier: pack.Identifier, packId: pack.PackageId, id: pack.Parameters.map(function (param) { return param.ParameterId; }) });
                            standartPacksId.push(pack.PackageId);
                        }
                    }
                    else 
                        for (var j = 0; j < pack.Parameters.length; j++) {
                            var p = pack.Parameters[j];
                            if (nonStandartParamsIds.indexOf(p.ParameterId) == -1) {
                                nonStandartParamsIds.push(p.ParameterId);
                                allParamsIdentifier.push({ identifier: p.Identifier, packId: pack.PackageId, id: [p.ParameterId] });
                            }
                        }
                }
                $scope.allParamsIdentifier = allParamsIdentifier;
                

                $scope.saveReceivedAmount = function (sampleId, samplingIdentifierId, receivedAmount, sampleType) {
                    if (typeof receivedAmount !== 'number' || Number.isNaN(receivedAmount)) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Valor no v&aacute;lido...', content: "La cantidad recibida no es un valor v&aacute;lido." });
                        return;
                    }

                    samplesFact.setReceivedAmount(sampleId, samplingIdentifierId, receivedAmount, sampleType)
                        .success(function (data) {
                            if (!data.success)
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Valor no v&aacute;lido...', content: "La cantidad recibida no es un valor v&aacute;lido." });
                        })
                        .error(function () {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });

                        });
                }
                $scope.saveLabNo = function (sampleId, samplingIdentifierId, labNo,  sampleType ) {
                    if (typeof labNo !== 'number' || Number.isNaN(labNo)) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Valor no v&aacute;lido...', content: "El n&uacute;mero de Laboratorio no es un valor v&aacute;lido." });
                        return;
                    }
                    samplesFact.setLabNo(sampleId, samplingIdentifierId, labNo, sampleType)
                        .success(function (data) {
                            if (!data.success)
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Valor no v&aacute;lido...', content: "La cantidad recibida no es un valor v&aacute;lido." });
                        })
                        .error(function () {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        });
                }
            }
        ]
    );

function concatArray(a, b) {
     return a.concat(b);
}