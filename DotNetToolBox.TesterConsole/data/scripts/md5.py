from struct import pack, unpack
from typing import BinaryIO
from io import BytesIO
from Crypto.Random import get_random_bytes
from Crypto.Hash import MD5


def read_lv(stream: BinaryIO):
    data_len = stream.read(4)
    value_len = unpack('i', data_len)[0]
    return stream.read(value_len)


def write_lv(stream: BinaryIO, value: bytes):
    data_len = pack('i', len(value))
    stream.write(data_len)
    stream.write(value)


def main():
    with open(r'C:\Temp\testData\md5.dat', 'wb') as file:
        for i in range(1, 1000 + 1):
            with BytesIO() as ms:
                data = get_random_bytes(i)
                write_lv(ms, data)
                md5 = MD5.new(data)
                write_lv(ms, md5.digest())

                ms.seek(0)
                record = ms.read()

            write_lv(file, record)
        write_lv(file, b'')


if __name__ == '__main__':
    main()

