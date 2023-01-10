﻿using JoberMQ.Library.StatusCode.Enums;
using JoberMQ.Library.StatusCode.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultStatusCodeConst
    {
        internal const StatusCodeFactoryEnum StatusCodeFactory = StatusCodeFactoryEnum.Default;
        internal const StatusCodeMessageLanguageEnum StatusCodeMessageLanguage = StatusCodeMessageLanguageEnum.tr;
        internal static ConcurrentDictionary<string, StatusCodeModel> DefaultStatusCodeDatas = DefaultStatusCodeData();
        private static ConcurrentDictionary<string, StatusCodeModel> DefaultStatusCodeData()
        {
            var datas = new ConcurrentDictionary<string, StatusCodeModel>();

            datas.TryAdd("0.0.1", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.1",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Sunucu başlatılamadı, bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "The server could not be started, an error occurred."
                    }
                }
            });

            datas.TryAdd("0.0.2", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.2",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veritabanı oluşturulurken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while creating the database."
                    }
                }
            });

            datas.TryAdd("0.0.3", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.3",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veriler içe aktarılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while importing data."
                    }
                }
            });

            datas.TryAdd("0.0.4", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.4",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Zamanlanmış görevler oluşturulurken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while creating scheduled tasks."
                    }
                }
            });

            datas.TryAdd("0.0.5", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.5",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veri kontrolü yapılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while checking the data."
                    }
                }
            });

            datas.TryAdd("0.0.6", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.6",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veri kontrol zamanlayıcısı başlatılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while starting the data check timer."
                    }
                }
            });

            datas.TryAdd("0.0.7", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.7",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Mesajlar içe aktarılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while importing messages."
                    }
                }
            });

            datas.TryAdd("0.0.8", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.8",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Hata mesajları içe aktarılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while importing error messages."
                    }
                }
            });

            datas.TryAdd("0.0.9", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.9",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Tamamlanan görevleri silecek zamanlayıcı başlatılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while starting the scheduler that will delete completed tasks."
                    }
                }
            });

            datas.TryAdd("0.0.10", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.10",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Aynı ClientKey'e sahip, birden fazla oturum açamaz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "Cannot login more than one with the same ClientKey."
                    }
                }
            });

            datas.TryAdd("0.0.11", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.11",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Kullanıcı bilgileriniz yanlış."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "Your user information is incorrect."
                    }
                }
            });

            datas.TryAdd("0.0.12", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.12",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Sunucuya erişilemedi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "The server could not be reached."
                    }
                }
            });

            datas.TryAdd("0.0.13", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.13",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Sunucu hazırlanıyor, erişemezsiniz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "The server is being prepared, you cannot access it."
                    }
                }
            });

            return datas;
        }
    }
}
