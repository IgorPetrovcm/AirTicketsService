import os
import json
from getpass import getpass
from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_OAEP

rsakeys = RSA.generate(2048)
private_key_bytes = rsakeys.export_key()

public_key = rsakeys.public_key()
public_key_bytes = public_key.export_key()


app_conf_dir = os.path.expanduser("~") + "/.airtickets_mailconf"
if os.path.exists(app_conf_dir) == False:
    os.mkdir( app_conf_dir )
    with open( app_conf_dir + "/privatekey.pem", "w") as file_stream:
        file_stream.write( private_key_bytes.decode("utf-8") )
    with open( app_conf_dir + "/publickey.pem", "w") as file_stream:
        file_stream.write( public_key_bytes.decode("utf-8") )

email_address = input("Email address: ")
email_password_bytes = getpass(prompt="Email password: ").encode("utf-8")

cipher = PKCS1_OAEP.new(public_key)

email_password_encrypt = cipher.encrypt( email_password_bytes )

mail_conf_dict = {
    "address": email_address,
    "password": email_password_encrypt.decode("CP866")
    }

with open( app_conf_dir + "/mail_settings.json", "w") as file_stream:
    json.dump(mail_conf_dict, file_stream)
