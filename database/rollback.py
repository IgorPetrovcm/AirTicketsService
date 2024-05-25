from dotenv import load_dotenv
import os

if os.path.exists("container.env") == False:
    raise Exception("File ""container.env"" does not exists")

print("Loading environment variable from ""container.env""\n")

load_dotenv("container.env")

IMAGE_NAME = os.getenv("IMAGE_NAME")
CONTAINER_NAME = os.getenv("CONTAINER_NAME")

os.system("sudo docker container stop " + CONTAINER_NAME)
os.system("sudo docker container rm " + CONTAINER_NAME)

os.system("sudo docker rmi " + IMAGE_NAME)

if os.path.exists("dump.sql") == True:
    os.remove("dump.sql")