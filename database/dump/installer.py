from urllib.request import urlretrieve
from dotenv import load_dotenv
from sys import argv
import os
import zipfile

DUMP_FILE_NAME = "dump.zip"

dump_size = argv[1]
path_to_env = argv[2]

if ( os.path.exists( path_to_env ) == False):
    raise FileNotFoundError(path_to_env," Not found")

load_dotenv(path_to_env)

URL_FOR_SMALL_DEMO = os.getenv("URL_FOR_SMALL_DEMO")
URL_FOR_MEDIUM_DEMO = os.getenv("URL_FOR_MEDIUM_DEMO")
URL_FOR_BIG_DEMO = os.getenv("URL_FOR_BIG_DEMO")

urls = {
    "small" : URL_FOR_SMALL_DEMO,
    "medium": URL_FOR_MEDIUM_DEMO,
    "big" : URL_FOR_BIG_DEMO
}

if dump_size in urls:
    print("Installation dump at ", urls[dump_size])
else:
    raise Exception("The dump size argument is not correct")

try:
    urlretrieve(urls[dump_size], DUMP_FILE_NAME)
except OSError as error: 
    print(error)
finally:
    with zipfile.ZipFile(DUMP_FILE_NAME, "r") as zip_stream:
        zip_stream.extractall()
        file_names_in_zip = zip_stream.namelist()
        os.rename( file_names_in_zip[0], "dump.sql")
        os.remove(DUMP_FILE_NAME)
