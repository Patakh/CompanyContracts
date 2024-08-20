using CompanyContractsDAL;

while (true)
{
    Console.WriteLine("Выберите действие:");
    Console.WriteLine("1. Вывести сумму всех заключенных договоров за текущий год.");
    Console.WriteLine("2. Вывести сумму заключенных договоров по каждому контрагенту из России.");
    Console.WriteLine("3. Вывести список e-mail уполномоченных лиц, заключивших договоры за последние 30 дней, на сумму больше 40000.");
    Console.WriteLine("4. Изменить статус договора на \"Расторгнут\" для физических лиц, у которых есть действующий договор, и возраст которых старше 60 лет включительно.");
    Console.WriteLine("5. Создать отчет (текстовый файл, например, в формате xml, xlsx, json) содержащий ФИО, e-mail, моб. телефон, дату рождения физ. лиц, у которых есть действующие договора по компаниям, расположенных в городе Москва.");
    Console.WriteLine("6. Выход.");

    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            Console.WriteLine($"Сумма договоров за текущий год: {DataWorker.GetTotalContractAmountForCurrentYear()}");
            break;
        case "2":
            var contractorAmounts = DataWorker.GetTotalContractAmountByRussianContractor();
            foreach (var contractor in contractorAmounts)
            {
                Console.WriteLine($"Контрагент: {contractor.Key}, Сумма: {contractor.Value}");
            }
            break;
        case "3":
            var emails = DataWorker.GetEmailsOfAuthorizedPersonsForRecentContracts();
            Console.WriteLine("E-mail уполномоченных лиц:");
            foreach (var email in emails) 
                Console.WriteLine(email);
            break;
        case "4":
            DataWorker.TerminateContractsForElderlyPersons();
            Console.WriteLine("Статус договоров для физических лиц старше 60 лет изменен на \"Расторгнут\".");
            break;
        case "5":
            Console.WriteLine("Выберите формат отчета (json, xml):");
            var reportFormat = Console.ReadLine();
            Console.WriteLine("Введите имя файла для отчета:");
            var fileName = Console.ReadLine();
            switch (reportFormat)
            {
                case "json":
                    DataWorker.CreateReportToJson(fileName);
                    Console.WriteLine($"Отчет в формате JSON сохранен в файл {fileName}");
                    break;
                case "xml":
                    DataWorker.CreateReportToXml(fileName);
                    Console.WriteLine($"Отчет в формате XML сохранен в файл {fileName}");
                    break;
                default:
                    Console.WriteLine("Неверный формат отчета.");
                    break;
            }
            break;
        case "6":
            Console.WriteLine("Выход из приложения.");
            return;
        default:
            Console.WriteLine("Неверный выбор.");
            break;
    }
}