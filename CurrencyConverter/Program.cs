using CurrencyConverter;
using CurrencyConverter.Repositories;
using CurrencyConverter.Services;

ICurrencyRateRepository currencyRateRepository = new CurrencyRepository(HardcodedData.CurrencyDictionary);
ConversionService conversionService = new ConversionService(currencyRateRepository);
WorkflowService workflowService = new WorkflowService(conversionService);

workflowService.Start();