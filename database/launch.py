# PGPASSWORD=root psql -h localhost -p 5432 -U root -d postgres < dump.sql

from sys import argv
from dotenv import load_dotenv
import os

USERNAME = "root"
PASSWORD = "root"
IMAGE_NAME = "demo_db_image"
CONTAINER_NAME = "demo_db_container"
PORT = "5432"
DUMP_SIZE = "small"

if os.path.exists("container.env"):
    print("Loading environment variable from ""container.env""\n")
    load_dotenv("container.env")
    USERNAME = os.getenv("DMBS_USERNAME")
    PASSWORD = os.getenv("DMBS_PASSWORD")
    IMAGE_NAME = os.getenv("IMAGE_NAME")
    CONTAINER_NAME = os.getenv("CONTAINER_NAME")
    PORT = os.getenv("PORT")

os.system("python ./dump/installer.py " + DUMP_SIZE + " ./dump_installer.env")


os.system("sudo docker build -t " + IMAGE_NAME + " --build-arg DMBS_USER=" + USERNAME + " --build-arg DMBS_PASSWORD=" + PASSWORD + " .    ")
print("Creted docker image: " + IMAGE_NAME + "\n")

os.system("sudo docker run -d --name " + CONTAINER_NAME + " -p " + PORT + ":5432 " + IMAGE_NAME)
print("Created docker container: " + CONTAINER_NAME + "\n")
