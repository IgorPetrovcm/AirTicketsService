# Database dump demo installer

## Installation instructions

### Setting environment

Для работы понадобится установить:
+ postgresql
+ docker
+ python

Для **python** нам также понадобится библиотека "python-dotenv", которую можно скачать с помощью любого популярного менеджера пакетов.

Например - pip:
```shell
pip install --break-system-packages -r requirements.txt
```

Для удобства и корректной работы следует установить используя файл "requirements.txt".

### Configuration

Проект содержит файл **"launch.py"** для запуска настройки всей среды с базой данных.

Файл **"load_dump.sh"** - скрипт загрузки дампа базы данных

**"Dokerfile"** стандарный докер-файл

**"dump_installer.env"** файл хранит переменные среды, содержажие ссылки на файлы дампы

#### Настройка переменных окружения базы данных
По умолчанию скрипт **launch.py** задает следующие значения для доступа к контейнеру с бд:
+ **DMBS_USERNAME**: Имя пользователя в СУБД (root)
+ **DMBS_PASSWORD**: Пароль от пользователя в СУБД (root)
+ **IMAGE_NAME**: Название image в докер (demo_db_image)
+ **CONTAINER_NAME**: Название контейнера в докер(demo_db_container)
+ **PORT**: Внешний порт для подключения(5432)
+ **DUMP_SIZE**: размен дампа small, medium, big (small)

Если вы хотите написать свою конфигурацию - создайте файл **"container.env"** и используйте в нем выше описанные переменные, например: 
```
DMBS_USERNAME=name
DMBS_PASSWORD=1234
IMAGE_NAME=image_demo
CONTAINER_NAME=container_demo
PORT=23144
DUMP_SIZE=medium
```

# Launch

1. Сначала запустите **"launch.py"** ОБЯЗАТЕЛЬНО ИСПОЛЬЗУЯ SUDO и дождитесь полной загрузки
```shell
sudo python launch.py
```
2. Запустите **""load_dump.sh**, после этого база данных готова для работы:
```shell
sh load_dump.sh
```