from urllib.request import urlretrieve
from dotenv import load_dotenv
from sys import argv
import os
import zipfile

DUMP_FILE_NAME = "dump.zip"
argument = argv[1]

load_dotenv("appconf.env")

URL_FOR_SMALL_DEMO = os.getenv("URL_FOR_SMALL_DEMO")
URL_FOR_MEDIUM_DEMO = os.getenv("URL_FOR_MEDIUM_DEMO")
URL_FOR_BIG_DEMO = os.getenv("URL_FOR_BIG_DEMO")

urls = {
    "small" : URL_FOR_SMALL_DEMO,
    "medium": URL_FOR_MEDIUM_DEMO,
    "big" : URL_FOR_BIG_DEMO
}

if argument in urls:
    print("Installation dump at ", urls[argument])
else:
    raise Exception("The argument is not correct")

try:
    urlretrieve(urls[argument], DUMP_FILE_NAME)
except OSError as error: 
    print(error)
finally:
    with zipfile.ZipFile(DUMP_FILE_NAME, "r") as zip_stream:
        zip_stream.extractall()
        os.remove(DUMP_FILE_NAME)
