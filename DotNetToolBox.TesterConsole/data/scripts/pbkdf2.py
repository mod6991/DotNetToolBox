from struct import pack, unpack
from typing import BinaryIO
from io import BytesIO
from Crypto.Random import get_random_bytes
from Crypto.Protocol.KDF import PBKDF2
from Crypto.Hash import SHA1


def read_lv(stream: BinaryIO):
    data_len = stream.read(4)
    value_len = unpack('i', data_len)[0]
    return stream.read(value_len)


def write_lv(stream: BinaryIO, value: bytes):
    data_len = pack('i', len(value))
    stream.write(data_len)
    stream.write(value)


def main():
    with open(r'C:\Temp\testData\pbkdf2.dat', 'wb') as file:
        for i in range(1, 300 + 1):
            with BytesIO() as ms:
                # data = get_random_bytes(i)
                # write_lv(ms, data)
                password = get_random_bytes(8).hex()
                write_lv(ms, bytes(password, 'ascii'))
                salt = get_random_bytes(16)
                write_lv(ms, salt)

                key = PBKDF2(password, salt, 32, count=50000, hmac_hash_module=SHA1)
                write_lv(ms, key)

                ms.seek(0)
                record = ms.read()

            write_lv(file, record)
        write_lv(file, b'')


if __name__ == '__main__':
    main()
