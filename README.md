- Приоритетными конечно являются GET запросы к серверу, но этот способ  далеко не у всех работает , либо блокируется, в частности bet365 создает подключение через сокеты (тунель)и дополнительно шифрует (потенциально можно разобраться с дешифровкой)

- Второй способ который пробовал : это управляемый кодом браузер (Chromium  Selenium), но опять же , bet365 распознает такие браузеры и блокирует соединение

- Следующий способ запускать JS скрипт на странице, в таком случае проблема - кросс-доменные запросы, которую я обошел при помощи расширения для браузера, которое отправляет к нам на сервер HTML код со всеми коэф. Страница никак не может узнать о том , какие расширения установлены в браузере , и поэму этот метод не будет заблокирован сайтом
Я сделал для одной страницы, в потенциале расширение может получить доступ ко всем ТАБам браузера.


