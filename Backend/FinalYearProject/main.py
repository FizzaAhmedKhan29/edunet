import os.path

from enlighten_inference import EnlightenOnnxModel
import cv2
from PIL import Image
import sys

try:
    if len(sys.argv) == 3:
        img = cv2.imread(sys.argv[1])
        model = EnlightenOnnxModel()
        processed = model.predict(img)
        im = Image.fromarray(processed)
        im.save(sys.argv[2])
except Exception as e:
    print(e)
