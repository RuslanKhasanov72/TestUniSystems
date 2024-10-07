<body>
    <div class="container">
        <p>Этот проект демонстрирует создание и запуск Docker контейнера для API. Он использует ASP.NET и Postgre SQL для реализации веб-API с базами данных зданий и комнат.</p>
        <h2>Инструкция по запуску</h2>
        <h3>1. Создание и запуск контейнера Docker</h3>
        <p>Для запуска контейнера используйте <code>docker-compose up --build</code> в папке с файлом docker-compose.yml. Введите команду:</p>
        <pre><code>docker-compose up --build</code></pre>
        <h3>2. Тестирование API</h3>
        <p>После запуска контейнера вы можете протестировать API Комнат, перейдя по следующему адресу в вашем браузере:</p>
        <p><a href="http://localhost:7211/swagger/index.html" target="_blank">http://localhost:7211/swagger/index.html</a></p>
        <p>Для теста API Зданий, перейдите по следующему адресу в вашем браузере:</p>
        <p><a href="http://localhost:7217/swagger/index.html" target="_blank">http://localhost:7217/swagger/index.html</a></p>
        <p>Для теста клиентского приложения(Не удалось получить лицензию Kendo UI, из-за этого воспользовался Bootstrap), перейдите по следующему адресу в вашем браузере:</p>
        <p><a href="http://localhost:4201" target="_blank">http://localhost:4201</a></p>
</body>
