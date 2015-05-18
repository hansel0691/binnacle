angular.module('bitacora.controllers.status', [])
    .controller('StatusCtrl', ['$scope', 'ordersFact', '$compile', 'loadingFact',
    function ($scope, ordersFact, $compile, loadingFact) {
        $scope.getStatusTitle = function () {
            if (!$scope.order) return "";
            switch ($scope.order.OrderState) {
                case 0:
                    return "Orden de Trabajo Sin Enviar";
                case 1:
                    return "Orden de Trabajo Enviada";
                case 2:
                    return "Orden de Trabajo Sin Evaluar";
                case 3:
                    return "Orden de Trabajo Evaluada";
                case 4:
                    return "Orden de Trabajo Sin Terminar";
                case 5:
                    return "Orden de Trabajo Incumplida";
                default:
                    return "";
            }
        }
        $scope.getStatusFooter = function () {
            if ($scope.order.OrderState == 2)
                return "Evaluar Muestreo";
            if ($scope.order.OrderState == 3)
                return "Ver Datos del Muestreo";
            if ($scope.order.OrderState == 4)
                return "Editar Orden de Trabajo";
            if ($scope.order.OrderState == 5)
                return "Opciones";
            return "";
        }
        $scope.getSatusAction = function () {
            if ($scope.order.OrderState == 2)
                return "Evaluar";
            if ($scope.order.OrderState == 3)
                return "Ver";
            if ($scope.order.OrderState == 4)
                return "Editar";
            if ($scope.order.OrderState == 5)
                return "Enviar";
            return "";
        }
        $scope.getStatusContent = function () {
            if (!$scope.order) return "";
            var now = getNowMls();
            var result = "";

            if ($scope.order.OrderState == 0)
                result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" no ha sido enviada al Muestreador a\u00FAn.';
            else if ($scope.order.OrderState == 1) {
                if (now < $scope.order.SamplingData.StartTime)
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" est\u00E1 pendiente a realizaci\u00F3n. La fecha de inicio del muestreo est\u00E1 fijada para el ' + $scope.getDate($scope.order.SamplingData.StartTime) + ' a las ' + $scope.getHours($scope.order.SamplingData.StartTime) + '.';
                else if ($scope.order.SamplingData.StartTime < now && now < $scope.order.SamplingData.EndTime)
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" est\u00E1 en tiempo de realizaci\u00F3n. La fecha final del muestreo est\u00E1 fijada para el ' + $scope.getDate($scope.order.SamplingData.EndTime) + ' a las ' + $scope.getHours($scope.order.SamplingData.EndTime) + ' aproximadamente.';
                else
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" est\u00E1 atrasada. La fecha final del muestreo estaba fijada para el ' + $scope.getDate($scope.order.SamplingData.EndTime) + ' a las ' + $scope.getHours($scope.order.SamplingData.EndTime) + ' aproximadamente y no se han recibido los datos del muestreador a\u00FAn.';
            }
            else if ($scope.order.OrderState == 2) {
                if ($scope.order.DataInformation.Header.fechaRealizacion < $scope.order.SamplingData.StartTime)
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" ha sido cumplida antes de tiempo. La fecha de inicio del muestreo estaba fijada para el ' + $scope.getDate($scope.order.SamplingData.StartTime) + ' - ' + $scope.getHours($scope.order.SamplingData.StartTime) + ' y fue enviada el ' + $scope.getDate($scope.order.DataInformation.Header.fechaRealizacion) + ' - ' + $scope.getHours($scope.order.DataInformation.Header.fechaRealizacion) + '.';
                else if ($scope.order.SamplingData.StartTime < $scope.order.DataInformation.Header.fechaRealizacion && $scope.order.DataInformation.Header.fechaRealizacion < $scope.order.SamplingData.EndTime)
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" ha sido cumplida antes de tiempo. La fecha de inicio del muestreo estaba fijada para el ' + $scope.getDate($scope.order.SamplingData.StartTime) + ' - ' + $scope.getHours($scope.order.SamplingData.StartTime) + ' y fue enviada el ' + $scope.getDate($scope.order.DataInformation.Header.fechaRealizacion) + ' - ' + $scope.getHours($scope.order.DataInformation.Header.fechaRealizacion) + '.';
                else
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" ha sido cumplida con atraso. La fecha final del muestreo estaba fijada para el ' + $scope.getDate($scope.order.SamplingData.EndTime) + ' - ' + $scope.getHours($scope.order.SamplingData.StartTime) + ' y fue enviada el ' + $scope.getDate($scope.order.DataInformation.Header.fechaRealizacion) + ' - ' + $scope.getHours($scope.order.DataInformation.Header.fechaRealizacion) + '.';
            }
            else if ($scope.order.OrderState == 3) {
                if ($scope.order.DataInformation.Header.fechaRealizacion < $scope.order.SamplingData.StartTime)
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" ha sido cumplida antes de tiempo. ';
                else if ($scope.order.SamplingData.StartTime < $scope.order.DataInformation.Header.fechaRealizacion && $scope.order.DataInformation.Header.fechaRealizacion < $scope.order.SamplingData.EndTime)
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" ha sido cumplida antes de tiempo. ';
                else
                    result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" ha sido cumplida con atraso. ';

                if ($scope.order.SamplingState == 2)
                    result += 'El Muestreo de esta orden ha sido evaluado de incorrecto.';
                else if ($scope.order.SamplingState == 1)
                    result += 'El Muestreo de esta orden ha sido evaluado de correcto.';
            }
            else if ($scope.order.OrderState == 4) {
                result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" no ha sido completada por el coodinador. La misma puede ser editada mediante el link que aparece m\u00E1s abajo.';
            }
            else if ($scope.order.OrderState == 5) {
                result += 'La Orden de Trabajo "' + $scope.order.SamplingData.Identifier + '" no ha sido cumplida por el muestreador. "' + $scope.order.DataInformation.Header.motivoIncumplida + '" \n Observaciones de incumplimiento: ' + $scope.order.DataInformation.Header.observacionIncumplida;

            }
            return result;

        }
        $scope.onAction = function () {
            if ($scope.order.OrderState == 2 || $scope.order.OrderState == 3)
                $scope.goToUrl('/order/' + $scope.order.Id + '/samples');
            if ($scope.order.OrderState == 4 || $scope.order.OrderState == 5)
                $scope.goToUrl('/edit/order/' + $scope.order.Id);
        }
    }])