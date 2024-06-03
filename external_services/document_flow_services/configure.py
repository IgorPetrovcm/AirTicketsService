print("Configure Data Bus Service:\n")
databus_user = input("Enter the username: ")
databus_passwd = input("Enter the password: ")
databus_management_port = input("Enter the management host port: ")
databus_port = input("Enter the data bus port: ")

with open(".env", "w") as file_stream:
    file_stream.write(f"""DATABUS_USERNAME={databus_user}
DATABUS_PASSWORD={databus_passwd}
DATABUS_MANAGEMENT_PORT={databus_management_port}
DATABUS_PORT={databus_port}
                    """)
