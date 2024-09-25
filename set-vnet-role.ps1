#===============================================================================
# Microsoft FastTrack for Azure
# Grant Azure Service Fabric Resource Provider Network Contributor rights to the
# subnets of a virtual network
#===============================================================================
# Copyright © Microsoft Corporation.  All rights reserved.
# THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
# OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
# LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
# FITNESS FOR A PARTICULAR PURPOSE.
#===============================================================================
param(
    [Parameter(Mandatory)]$subscriptionId,
    [Parameter(Mandatory)]$resourceGroupName,
    [Parameter(Mandatory)]$virtualNetworkName
)
Login-AzAccount
Select-AzSubscription -SubscriptionId $subscriptionId
$sp = Get-AzADServicePrincipal -DisplayName "Azure Service Fabric Resource Provider"
$vnet = Get-AzVirtualNetwork -Name $virtualNetworkName -ResourceGroupName $resourceGroupName
$ra1 = New-AzRoleAssignment -PrincipalId $sp.Id -RoleDefinitionName "Network Contributor" -Scope $vnet.subnets[0].Id
$ra2 = New-AzRoleAssignment -PrincipalId $sp.Id -RoleDefinitionName "Network Contributor" -Scope $vnet.subnets[1].Id