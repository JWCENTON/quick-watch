#!/bin/bash

# REPLACE VALUES WITH REAL KEY VAULT AND SUBSCRIPTION DATA

# source
source_vault_name=""
source_vault_subscription_name=""

# target
target_vault_name=""
target_vault_subscription_name=""

# folder where copy of every key will be saved - default ./SecretBackups
output_directory="./SecretBackups"

# create folder if missing
mkdir -p "$output_directory"

# change subscription to the source one - in case its different
az account set --subscription "$source_vault_subscription_name"

# List secrets and save the list to secret_list.txt
az keyvault secret list --vault-name "$source_vault_name" --query "[].name" --output tsv > secret_list.txt

while IFS= read -r secret_name; do
	# Remove Byte Order Mark (BOM) if present at the beginning of the text ONLY WHEN NEEDED
	# secret_name=$(echo "$secret_name" | sed 's/^\xEF\xBB\xBF//')
	
	# Remove any leading or trailing whitespace
	secret_name=$(echo "$secret_name" | sed -e 's/^[[:space:]]*//' -e 's/[[:space:]]*$//')
	# Replace invalid characters with underscores
	secret_name=$(echo "$secret_name" | tr -cd 'a-zA-Z0-9_.-')
	
	# download one key and save it to .txt file 
	az account set --subscription "Azure free subscription"
	az keyvault secret download --file "$output_directory/$secret_name.txt" --vault-name "$source_vault_name" -n "$secret_name"
	
	# upload one key that was downloaded above
	az account set --subscription "$target_vault_subscription_name"
	az keyvault secret set --vault-name "$target_vault_name" -n "$secret_name" --file "$output_directory/$secret_name.txt"

done < secret_list.txt

# Clean up: Remove the entire folder containing downloaded secrets (if needed)
# rm -r "$output_directory"