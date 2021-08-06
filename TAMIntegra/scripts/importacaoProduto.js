$(document).ready(function() {
            $('#myForm').bootstrapValidator({
                message: 'No es valido',
                framework: 'bootstrap',
                feedbackIcons: {
                    //valid: 'glyphicon glyphicon-ok',
                    //invalid: 'glyphicon glyphicon-remove',
                    //validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    tipoMovimentoFiltro: {
                        validators: {
                            notEmpty: {
                                message: 'Digite o tipo de movimento.'
                            },
                            stringLength: {
                                max: 6,
                                message: 'Este campo deve conter até 6 caracteres.'
                            }
                        }
                    },

                    numeroMovimentoFiltro: {
                        validators: {
                            notEmpty: {
                                message: 'Digite o número do movimento.'
                            },
                            stringLength: {
                                max: 100,
                                message: 'Este campo deve conter até 100 caracteres.'
                            }
                        }
                    },
					codigoFiltro: {
                        validators: {
                            stringLength: {
                                max: 10,
                                message: 'Este campo deve conter até 10 caracteres.'
                            }
                        }
                    },
					nomeFantasiaFiltro: {
                        validators: {
                            stringLength: {
                                max: 10,
                                message: 'Este campo deve conter até 10 caracteres.'
                            }
                        }
                    }
                }
            })
	}); 