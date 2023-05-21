#!/usr/bin/env python
# coding: utf-8

# In[4]:


# installing LightGBM
import sys
sys.path.append("C:\\Users\\harin\\AppData\\Local\\Programs\\Python\\Python311\\DLLs")
sys.path.append("C:\\Users\\harin\\AppData\\Local\\Programs\\Python\\Python311\\Lib\\site-packages")
import _ctypes
import lightgbm as lightgb
from sklearn.metrics import mean_squared_error
import matplotlib.pyplot as pyplt
from pandas import DataFrame
import pandas as pnd
import seaborn as sb
from sklearn.preprocessing import OneHotEncoder
from sklearn.preprocessing import LabelEncoder
from sklearn import svm
from sklearn.svm import SVC
from sklearn.metrics import mean_absolute_percentage_error
from sklearn.metrics import mean_absolute_error
from sklearn.model_selection import train_test_split
#from sklearn.ensemble import RandomForestRegressor
#from sklearn.linear_model import LinearRegression
import numpy as nump
from sklearn.datasets import make_blobs
import openpyxl
# create the inputs and outputs

# read data set from excel
data_set = pnd.read_excel("C:\\Users\\harin\\OneDrive\\Documents\\IIT\\ResearchProject\\w1867882_Harini_Hapuarachchi_Land_Evaluation\\Dataset\\Rubber.xlsx")

# print shape of the data set with mentinoning number of rows and columns.
print(data_set.shape)

# Get number of categorical attribute in data set and include the attribute column values to a list
obj_category = (data_set.dtypes == 'object')
obj_cols = list(obj_category[obj_category].index)

OH_encoder = OneHotEncoder(sparse=False)
OH_cols = pnd.DataFrame(OH_encoder.fit_transform(data_set[obj_cols]))
OH_cols.index = data_set.index
OH_cols.columns = OH_encoder.get_feature_names_out()

final_data = data_set.drop(obj_cols, axis=1)

final_data = pnd.concat([final_data,OH_cols], axis=1)
X, Y = make_blobs(n_samples=859, centers=26, n_features=26, random_state=26)
X = final_data.drop(['Class of Land Unit'], axis=1)
Y = final_data['Class of Land Unit']

# Split the training set into
# training and validation set
X_train, X_valid, Y_train, Y_valid = train_test_split(
    X, Y, train_size=0.75, test_size=0.15, random_state=0)

# defining parameters 
params = {
    'task': 'train', 
    'boosting': 'gbdt',
    'objective': 'regression',
    'num_leaves': 10,
    'learning_rate': 0.05,
    'metric': {'l2','l1'},
    'verbose': -1
}

# laoding data
lightgb_train = lightgb.Dataset(X_train, Y_train)
lightgb_eval = lightgb.Dataset(X_valid, Y_valid, reference=lightgb_train)

# fitting the model
model = lightgb.train(params,
                 train_set=lightgb_train,
                 valid_sets=lightgb_eval,
                 callbacks=[lightgb.early_stopping(stopping_rounds=30)])

# prediction
Y_pred = model.predict(X_valid)

# accuracy check
mean_sequred_err = mean_squared_error(Y_valid, Y_pred)
rm_sequred_err = mean_sequred_err**(0.5)
print("MSE: %.2f" % mean_sequred_err)
print("RMSE: %.2f" % rm_sequred_err)

rubber_input = pnd.read_excel("C:\\Users\\harin\\OneDrive\\Documents\\IIT\\ResearchProject\\w1867882_Harini_Hapuarachchi_Land_Evaluation\\Dataset\\Land Eveluation DataRubber Sample.xlsx")
# get prediction for new input
rubber_output = model.predict(rubber_input)
# summarize input and output
print(rubber_input)
print(rubber_output)


# In[ ]:




